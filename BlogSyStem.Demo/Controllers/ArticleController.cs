using BlogSyStem.BLL;
using BlogSyStem.Demo.Filters;
using BlogSyStem.Demo.Models.ArticleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace BlogSyStem.Demo.Controllers
{

    [BlogSyStemAuth]
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                IBLL.IArticleManager articleManager = new BLL.ArticleManager();
                var userid = Guid.Parse(Session["loginUserId"].ToString());
                articleManager.CreateCategory(model.CategoryName, userid);
                return RedirectToAction("CategoryList");
            }
            ModelState.AddModelError("", "您录入的消息有误");
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> CategoryList()
        {
            var userid = Guid.Parse(Session["loginUserId"].ToString());
            List<Dto.CategoryDto> listCategoryDto = await new BLL.ArticleManager().GetAllCategories(userid);
            return View(listCategoryDto);
        }

        [HttpGet]
        public async Task<ActionResult> CreateArticle()
        {
            var userid = Guid.Parse(Session["loginUserId"].ToString());
            ViewBag.CategoryIds = await new ArticleManager().GetAllCategories(userid);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]//不做检查
        public async Task<ActionResult> CreateArticle(ArticleViewModel model)
        {

            if (ModelState.IsValid)
            {
                var userid = Guid.Parse(Session["loginUserId"].ToString());
                //ViewBag.CategoryIds = await new ArticleManager().GetAllCategories(userid);
                await new ArticleManager().CreateArticle(model.Title, model.Context, model.CategoryIds, userid);
                return RedirectToAction("ArticleList1");
            }
            ModelState.AddModelError("", "您录入的消息有误");
            return View();
        }

        /// <summary>
        /// 自定义分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ArticleList1(int pageIndex = 0, int pageSize = 10)
        {
            #region 自定义分页
            IBLL.IArticleManager articleManager = new BLL.ArticleManager();
            var userid = Guid.Parse(Session["loginUserId"].ToString());
            List<Dto.ArticleDto> listCategoryDto = await articleManager.GetAllArticlesByUserId(userid, pageIndex, pageSize);
            //dataCount数据的总条数
            var dataCount = await articleManager.GetDataCount(userid);
            //PageCount 总页数
            ViewBag.PageCount = dataCount % pageSize == 0 ? dataCount / pageSize : dataCount / pageSize + 1;
            ViewBag.PageIndex = pageIndex;
            return View(listCategoryDto);

            #endregion
        }

        /// <summary>
        /// 第三方控件分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ArticleList2(int pageIndex = 1, int pageSize = 10)
        {
            var userid = Guid.Parse(Session["loginUserId"].ToString());
            #region 第三方控件分页
            IBLL.IArticleManager articleManager = new BLL.ArticleManager();
            //当前用户第pageIndex页数据 
            //List<Dto.ArticleDto> listCategoryDto = await articleManager.GetAllArticlesByUserId(userid, pageIndex - 1, pageSize);

            List<Dto.ArticleDto> listCategoryDto = await articleManager.GetAllArticles(userid);
            //dataCount数据的总条数
            //var dataCount = await articleManager.GetDataCount(userid);

            //第三方分页查询得到分页数据  将对象集合作为参数传入PagedList即可后端不用再写分页方法
            PagedList<Dto.ArticleDto> articleDtos = new PagedList<Dto.ArticleDto>(listCategoryDto, pageIndex, pageSize);
            return View(articleDtos);
            #endregion
        }

        /// <summary>
        /// 文章详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ArticleDeatlis(Guid? id)
        {
            var userid = Guid.Parse(Session["loginUserId"].ToString());
            ArticleManager articleManager = new ArticleManager();
            if (id == null || !await articleManager.ExistsArticle(id.Value))
            {
                return RedirectToAction(nameof(ArticleList2));
            }
            ViewBag.Comments = await articleManager.GetCommentModelListByArticleId(id.Value);
            ViewBag.Feel = articleManager.GetUserFeelToArticle(id.Value,userid);
            await articleManager.Read(id.Value, userid);
            Dto.ArticleDto articleDto = await articleManager.GetArticleModelById(id.Value);
            return View(articleDto);
        }

        [HttpGet]
        public async Task<ActionResult> EditArticle(Guid? id)
        {
            IBLL.IArticleManager articleManager = new BLL.ArticleManager();
            var userid = Guid.Parse(Session["loginUserId"].ToString());
            ViewBag.CategoryIds = await new ArticleManager().GetAllCategories(userid);
            var data = await articleManager.GetArticleModelById(id.Value);
            EditArticleViewModel model = new EditArticleViewModel()
            {
                Id = data.Id,
                Content = data.Content,
                CategoryIds = data.CategoryId,
                Title = data.Title
            };
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]//不做检查
        public async Task<ActionResult> EditArticle(EditArticleViewModel model)
        {

            if (ModelState.IsValid)
            {
                var userid = Guid.Parse(Session["loginUserId"].ToString());
                //ViewBag.CategoryIds = await new ArticleManager().GetAllCategories(userid);
                await new ArticleManager().EditArticle(model.Id, model.Title, model.Content, model.CategoryIds);
                return RedirectToAction("ArticleList2");
            }
            else
            {
                var userid = Guid.Parse(Session["loginUserId"].ToString());
                ViewBag.CategoryIds = await new ArticleManager().GetAllCategories(userid);
                return View(model);
            }

        }

        /// <summary>
        /// 发表评论
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task SendComment(Models.ArticleViewModels.CommentViewModel model)
        {
            var userid = Guid.Parse(Session["loginUserId"].ToString());
            IBLL.IArticleManager articleManager = new ArticleManager();
            await articleManager.CreateComment(userid, model.Id, model.Content);
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="guid">删除文章的Id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task DeleteArticle(Guid id)
        {
            IBLL.IArticleManager articleManager = new BLL.ArticleManager();
            await articleManager.RemoveArticle(id);
        }

        #region 表态
        /// <summary>
        /// 赞
        /// </summary>
        [HttpPost]
        public async Task GoodCount(Guid id)
        {
            var userid = Guid.Parse(Session["loginUserId"].ToString());
            IBLL.IArticleManager articleManager = new BLL.ArticleManager();
            await articleManager.GoodCount(id, userid);
        }

        /// <summary>
        /// 踩
        /// </summary>
        [HttpPost]
        public async Task BadCount(Guid id)
        {
            var userid = Guid.Parse(Session["loginUserId"].ToString());
            IBLL.IArticleManager articleManager = new BLL.ArticleManager();
            await articleManager.BadCount(id, userid);
            //return Json(new { result = "ok" });
        }

        /// <summary>
        /// 取消赞和踩
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Cancel(Guid id)
        {
            var userid = Guid.Parse(Session["loginUserId"].ToString());
            IBLL.IArticleManager articleManager = new BLL.ArticleManager();
            await articleManager.NoAttitude(id, userid);
        }
        #endregion

        #region bootstarp-table
        [HttpGet]
        public ActionResult ArticleList3()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ArticleListTable(int limit, int offset)
        {
            var userid = Guid.Parse(Session["loginUserId"].ToString());
            IBLL.IArticleManager articleManager = new BLL.ArticleManager();
            List<Dto.ArticleDto> listCategoryDto = await articleManager.GetAllArticlesByUserId(userid);
            var total = listCategoryDto.Count;
            var rows = listCategoryDto.Skip(offset).Take(limit).ToList();
            return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDepartment(int limit, int offset, string departmentname, string statu)
        {
            var lstRes = new List<Department>();
            for (var i = 0; i < 50; i++)
            {
                var oModel = new Department();
                oModel.ID = Guid.NewGuid();
                oModel.Name = "销售部" + i;
                oModel.Level = i.ToString();
                oModel.Desc = "暂无描述信息";
                lstRes.Add(oModel);
            }
            var total = lstRes.Count;
            var rows = lstRes.Skip(offset).Take(limit).ToList();
            //return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
            var a = Json(new { total = total, rows = rows });
            return a;
        }

        public class Department
        {
            public Guid ID { get; set; }

            public string Name { get; set; }

            public string Level { get; set; }

            public string Desc { get; set; }

        }

        #endregion
    }

}