using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;


namespace Guoke
{

    [ValidateInput(false)]
    public class ManageControllerBase<TService, TEntity> : Controller
        where TEntity : EntityBase
        where TService : EntityServiceBase<TEntity>, new()
    {

        public ActionResult JsonP(object jsonObject)
        {
            string json = JsonConvert.SerializeObject(jsonObject, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,

            });
            string callback = Request.Params["callback"];
            if (string.IsNullOrWhiteSpace(callback))
            {
                return Content(json);
            }
            return Content(string.Format("{0}({1})", callback, json));
        }

        protected TService Service { get; set; }

        protected int PageSize { get { return 20; } }


        public ManageControllerBase()
        {
            Service = new TService();
            
        }


        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Create(TEntity model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.Create(model);
                    return JsonP(new { result = true, message = "创建成功" });
                }
                else
                {
                    throw new Exception("模型验证失败");
                }
                

            }
            catch (Exception err)
            {
                ModelState.AddModelError("", string.Format("数据创建失败,{0}", err.Message));
            }
            return JsonP(new { result =false,message=ModelState.ToString() });
        }

        [HttpPost]
        public virtual ActionResult Edit(TEntity model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.Update(model);
                    return JsonP(new { result = true, message = "数据更新成功" });
                }
                else
                {
                    throw new Exception("模型验证失败");
                }


            }
            catch (Exception err)
            {
                ModelState.AddModelError("", string.Format("数据更新失败,{0}", err.Message));
            }
            return JsonP(new { result = false, message = ModelState.ToString() });
        }

        [HttpPost]
        public virtual ActionResult Delete(int id)
        {
            try
            {
                Service.Delete(id);
                return JsonP(new { result = true, message = "删除成功" });

            }
            catch (Exception err)
            {
                ModelState.AddModelError("", string.Format("数据删除失败,{0}", err.Message));
            }
            return JsonP(new { result = false, message = ModelState.ToString() });
        }


        [HttpPost]
        public virtual ActionResult DeleteList(int[] idList)
        {
            try
            {
                Service.DeleteList(idList);
                return JsonP(new { result = true, message = "批量删除成功" });

            }
            catch (Exception err)
            {
                ModelState.AddModelError("", string.Format("数据删除失败,{0}", err.Message));
            }
            return JsonP(new { result = false, message = ModelState.ToString() });
        }
        
        public virtual ActionResult PageList(int page = 1,int rows=10)
        {
            var list = Service.GetList().OrderBy(m => m.Sort).ThenByDescending(m=>m.ID).ToPagedList(page, rows);
            return JsonP(new { rows = list, total = list.TotalItemCount });
        }


    }
}
