using BlogSyStem.DAL;
using BlogSyStem.Dto;
using BlogSyStem.IBLL;
using BlogSyStem.IDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.WebControls.Mvc;

namespace BlogSyStem.BLL
{
    public class ArticleManager : IArticleManager
    {
        #region 文章类别
        /// <summary>
        /// 创建分类
        /// </summary>
        /// <param name="name"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task CreateCategory(string name, Guid userId)
        {
            using (IDAL.ICategoryService categorySvc = new CategoryService())
            {
                await categorySvc.CreateAsync(new Models.Category()
                {
                    CategoryName = name,
                    UserId = userId
                });
            }
        }

        /// <summary>
        /// 编辑类别
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="newcategoryName"></param>
        /// <returns></returns>
        public Task EditCategory(Guid categoryId, string newcategoryName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除类别
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public Task RemoveCategory(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取用户的所有分类
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<CategoryDto>> GetAllCategories(Guid userId)
        {
            using (IDAL.ICategoryService categoryService = new CategoryService())
            {
                return await categoryService.GetAllAsync().Where(arg => arg.UserId == userId).Select(arg => new Dto.CategoryDto()
                {
                    Id = arg.Id,
                    CategoryName = arg.CategoryName
                }).ToListAsync();
            }
        }

        #endregion

        #region 文章
        /// <summary>
        /// 写文章
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="categoryIds"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task CreateArticle(string title, string content, Guid[] categoryIds, Guid userId)
        {
            using (IDAL.IArticleService articleSvc = new ArticleService())
            {
                var article = new Models.Article()
                {
                    Title = title,
                    Content = content,
                    UserId = userId
                };
                await articleSvc.CreateAsync(article);
                Guid articleId = article.Id;
                using (var articleTocategorySvc = new ArticleToCategoryService())
                {
                    foreach (var categoryId in categoryIds)
                    {
                        await articleTocategorySvc.CreateAsync(new Models.ArticleToCategory()
                        {
                            ArticleId = articleId,
                            CategoryId = categoryId
                        }, save: false);
                    }
                    await articleTocategorySvc.Save();
                }
            }
        }

        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="ArticleId">文章Id</param>
        /// <param name="title">文章标题</param>
        /// <param name="content">文章内容</param>
        /// <param name="categoryIds">文章分类字符数组</param>
        /// <returns></returns>
        public async Task EditArticle(Guid ArticleId, string title, string content, Guid[] categoryIds)
        {
            using (IDAL.IArticleService articleService = new DAL.ArticleService())
            {
                var article = await articleService.GetModelByIdAsync(ArticleId);
                article.Title = title;
                article.Content = content;
                await articleService.EditAsync(article);

                using (IDAL.IArticleToCategoryService articleToCategoryService = new DAL.ArticleToCategoryService())
                {
                    //删除原有类别
                    foreach (var item in articleToCategoryService.GetAllAsync().Where(m => m.ArticleId == ArticleId))
                    {
                        await articleToCategoryService.RemoveAsync(item, false);
                    }

                    foreach (var item in categoryIds)
                    {
                        await articleToCategoryService.CreateAsync(new Models.ArticleToCategory() { ArticleId = ArticleId, CategoryId = item }, false); ;
                    }
                    await articleToCategoryService.Save();
                }
            }
        }

        /// <summary>
        /// 根据Id判断文章是否存在
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public async Task<bool> ExistsArticle(Guid articleId)
        {
            using (IDAL.IArticleService articleService = new DAL.ArticleService())
            {
                return await articleService.GetAllAsync().AnyAsync(m => m.Id == articleId);
            }
        }

        /// <summary>
        /// 根据用户邮箱获取文章列表
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<List<ArticleDto>> GetAllArticlesByUserEmail(string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取分类所有文章
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public Task<List<ArticleDto>> GetAllArticlesByCategoryId(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据Id获取文章
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public async Task<ArticleDto> GetArticleModelById(Guid articleId)
        {
            using (IDAL.IArticleService articleService = new DAL.ArticleService())
            {
                var data = await articleService.GetAllAsync().Where(m => m.Id == articleId).Select(
                    m => new Dto.ArticleDto()
                    {
                        Id = m.Id,
                        BadCount = m.BadCount,
                        Title = m.Title,
                        Content = m.Content,
                        Email = m.User.Email,
                        CreateTime = m.CreateTime,
                        ImagePath = m.User.ImagePath,
                        UserId = m.UserId
                    }).FirstAsync();
                using (IArticleToCategoryService articleToCategoryService = new ArticleToCategoryService())
                {
                    //Include链表查询
                    var cates = articleToCategoryService.GetAllAsync().Include(m => m.Category).Where(arg => arg.ArticleId == data.Id);
                    data.CategoryId = cates.Select(s => s.CategoryId).ToArray();
                    data.CategoryName = cates.Select(s => s.Category.CategoryName).ToArray();

                }
                using (IFeelService feelService = new FeelService())
                {
                    var list = feelService.GetAllAsync().Include(m => m.Article).Where(arg => arg.ArticleId == data.Id);
                    var GoodCount = list.Where(arg => arg.FeelType == Enumeration.FeelEnum.Good).Count();
                    var BadCount = list.Where(arg => arg.FeelType == Enumeration.FeelEnum.Bad).Count();
                    data.GoodCount = GoodCount;
                    data.BadCount = BadCount;
                    return data;
                }
            }
        }

        /// <summary>
        /// 获取文章的总条数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> GetDataCount(Guid userId)
        {
            using (IDAL.IArticleService articleService = new ArticleService())
            {
                return await articleService.GetAllAsync().CountAsync(m => m.UserId == userId);
            }
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public async Task RemoveArticle(Guid articleId)
        {
            using (IDAL.IArticleService articleService = new DAL.ArticleService())
            {
                await articleService.RemoveAsync(articleId);
            }
        }

        /// <summary>
        /// 获得该用户的所有文章
        /// 自定义分页查询 返回一个List
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<List<ArticleDto>> GetAllArticlesByUserId(Guid userId, int pageIndex, int pageSize)
        {
            using (IDAL.IArticleService articleService = new ArticleService())
            {
                var list = await articleService.GetAllByPageOrderAsync(pageSize, pageIndex, false).Where(arg => arg.UserId == userId)
                .Select(arg => new Dto.ArticleDto()
                {
                    Title = arg.Title,
                    //BadCount = arg.BadCount,
                    //GoodCount = arg.GoodCount,
                    Email = arg.User.Email,
                    Content = arg.Content,
                    CreateTime = arg.CreateTime,
                    Id = arg.Id,
                    ImagePath = arg.User.ImagePath,
                    UserId = arg.UserId
                }).ToListAsync();

                using (IArticleToCategoryService articleToCategoryService = new ArticleToCategoryService())
                {
                    foreach (var articleDto in list)
                    {
                        //Include链表查询
                        var data = articleToCategoryService.GetAllAsync().Include(m => m.Category).Where(arg => arg.ArticleId == articleDto.Id);
                        articleDto.CategoryId = data.Select(s => s.CategoryId).ToArray();
                        articleDto.CategoryName = data.Select(s => s.Category.CategoryName).ToArray();
                    }
                }
                using (IFeelService feelService = new FeelService())
                {
                    foreach (var item in list)
                    {
                        var _list = feelService.GetAllAsync().Include(m => m.Article).Where(arg => arg.ArticleId == item.Id);
                        var GoodCount = _list.Where(arg => arg.FeelType == Enumeration.FeelEnum.Good).Count();
                        var BadCount = _list.Where(arg => arg.FeelType == Enumeration.FeelEnum.Bad).Count();
                        item.GoodCount = GoodCount;
                        item.BadCount = BadCount;
                    }
                    return list;
                }
            }
        }


        /// <summary>
        /// 根据用户Id获得该用户的所有文章
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<ArticleDto>> GetAllArticlesByUserId(Guid userId)
        {
            using (IDAL.IArticleService articleService = new ArticleService())
            {
                var list = await articleService.GetAllAsync().Where(arg => arg.UserId == userId)
                .Select(arg => new Dto.ArticleDto()
                {
                    Title = arg.Title,
                    //BadCount = arg.BadCount,
                    //GoodCount = arg.GoodCount,
                    Email = arg.User.Email,
                    Content = arg.Content,
                    CreateTime = arg.CreateTime,
                    Id = arg.Id,
                    ImagePath = arg.User.ImagePath,
                    UserId = arg.UserId
                }).ToListAsync();

                using (IArticleToCategoryService articleToCategoryService = new ArticleToCategoryService())
                {
                    foreach (var articleDto in list)
                    {
                        //Include链表查询
                        var data = articleToCategoryService.GetAllAsync().Include(m => m.Category).Where(arg => arg.ArticleId == articleDto.Id);
                        articleDto.CategoryId = data.Select(s => s.CategoryId).ToArray();
                        articleDto.CategoryName = data.Select(s => s.Category.CategoryName).ToArray();
                    }
                }

                using (IFeelService feelService = new FeelService())
                {
                    foreach (var item in list)
                    {
                        var _list = feelService.GetAllAsync().Include(m => m.Article).Where(arg => arg.ArticleId == item.Id);
                        var GoodCount = _list.Where(arg => arg.FeelType == Enumeration.FeelEnum.Good).Count();
                        var BadCount = _list.Where(arg => arg.FeelType == Enumeration.FeelEnum.Bad).Count();
                        item.GoodCount = GoodCount;
                        item.BadCount = BadCount;
                    }
                }

                //文章阅读数量
                using (IReadingInfoService readingInfoService = new ReadingInfoService())
                {
                    foreach (var item in list)
                    {
                        var _list = readingInfoService.GetAllAsync().Where(arg => arg.ArticleId == item.Id);
                        item.ReadingVolume = _list.Count();
                    }
                    return list;
                }

            }

        }

        /// <summary>
        /// 获取除自己外的所有文章
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<ArticleDto>> GetAllArticles(Guid userId)
        {
            using (IDAL.IArticleService articleService = new ArticleService())
            {
                var list = await articleService.GetAllAsync().Where(arg => arg.UserId != userId)
                .Select(arg => new Dto.ArticleDto()
                {
                    Title = arg.Title,
                    //BadCount = arg.BadCount,
                    //GoodCount = arg.GoodCount,
                    Email = arg.User.Email,
                    CreateTime = arg.CreateTime,
                    Id = arg.Id,
                    Content=arg.Content,
                    ImagePath = arg.User.ImagePath,
                    UserId = arg.UserId,
                    NickName=arg.User.NickName
                }).ToListAsync();

                //文章主要内容
                foreach (var atem in list)
                {
                    if (atem.Content!=null&& atem.Content!="")
                    {
                        List<string> con = new List<string>();
                        foreach (var etem in atem.Content.Split('<'))
                        {
                            con.Add(etem);
                        }
                        atem.Content = con[0];
                    }
                }

                //文章类别
                using (IArticleToCategoryService articleToCategoryService = new ArticleToCategoryService())
                {
                    foreach (var articleDto in list)
                    {
                        //Include链表查询
                        var data = articleToCategoryService.GetAllAsync().Where(arg => arg.ArticleId == articleDto.Id);
                        articleDto.CategoryId = data.Select(s => s.CategoryId).ToArray();
                        articleDto.CategoryName = data.Select(s => s.Category.CategoryName).ToArray();
                    }
                }

                //文章赞成反对数量
                using (IFeelService feelService = new FeelService())
                {
                    foreach (var item in list)
                    {
                        var _list = feelService.GetAllAsync().Where(arg => arg.ArticleId == item.Id);
                        var GoodCount = _list.Where(arg => arg.FeelType == Enumeration.FeelEnum.Good).Count();
                        var BadCount = _list.Where(arg => arg.FeelType == Enumeration.FeelEnum.Bad).Count();
                        var Feel = GetUserFeelToArticle(item.Id, userId);
                        item.Feel = Feel;
                        item.GoodCount = GoodCount;
                        item.BadCount = BadCount;
                    }
                }

                //文章阅读数量
                using (IReadingInfoService readingInfoService = new ReadingInfoService())
                {
                    foreach (var item in list)
                    {
                        var _list = readingInfoService.GetAllAsync().Where(arg => arg.ArticleId == item.Id);
                        item.ReadingVolume = _list.Count();
                    }
                }

                //文章评论量
                using (ICommentService commentService = new CommentService())
                {
                    foreach (var item in list)
                    {
                        var _list = commentService.GetAllAsync().Where(arg => arg.ArticleId == item.Id);
                        item.CommentVolume = _list.Count();
                    }
                    return list;
                }
            }
        }
        #endregion

        #region 评论
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="articleId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task CreateComment(Guid userId, Guid articleId, string content)
        {
            using (IDAL.ICommentService commentService = new CommentService())
            {
                await commentService.CreateAsync(new Models.Comment()
                {
                    UserId = userId,
                    ArticleId = articleId,
                    Content = content
                });
            }
        }

        /// <summary>
        /// 评论列表
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public async Task<List<CommentDto>> GetCommentModelListByArticleId(Guid articleId)
        {
            using (IDAL.ICommentService commentService = new CommentService())
            {
                return await commentService.GetAllByOrderAsync(false).Where(m => m.ArticleId == articleId)
                    .Include(m => m.User)
                    .Select(m => new CommentDto()
                    {
                        Id = m.Id,
                        Content = m.Content,
                        CreateTime = m.CreateTime,
                        ImagePath = m.User.ImagePath,
                        Email = m.User.Email,
                        NickNamw = m.User.NickName
                    }).ToListAsync();
            }
        }
        #endregion

        #region 初始赞成反对
        /// <summary>
        /// 赞
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public async Task GoodCount(Guid articleId)
        {
            using (IDAL.IArticleService articleService = new DAL.ArticleService())
            {
                var article = await articleService.GetModelByIdAsync(articleId);
                article.GoodCount++;
                await articleService.EditAsync(article);
            }
        }

        /// <summary>
        /// 踩
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public async Task BadCount(Guid articleId)
        {
            using (IDAL.IArticleService articleService = new DAL.ArticleService())
            {
                var article = await articleService.GetModelByIdAsync(articleId);
                article.BadCount++;
                await articleService.EditAsync(article);
            }
        }
        #endregion

        #region 赞成反对
        /// <summary>
        /// 赞成
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public async Task GoodCount(Guid articleId, Guid userId)
        {
            using (IDAL.IFeelService feelService = new DAL.FeelService())
            {
                //先判断以前有没有表态 如果表态了就是已经存在的就只需要修改 如果是首次表态就是增加
                var temp = feelService.GetAllAsync().FirstOrDefault(arg => arg.ArticleId == articleId && arg.UserId == userId);
                if (temp != null)
                {
                    if (temp.FeelType == Enumeration.FeelEnum.Bad)
                    {
                        //如果此时是反对状态先取消反对 第二次点击才是赞成
                        temp.FeelType = Enumeration.FeelEnum.None;
                        await feelService.EditAsync(temp);
                    }
                    else
                    {
                        temp.FeelType = Enumeration.FeelEnum.Good;
                        await feelService.EditAsync(temp);
                    }
                }
                else
                {
                    await feelService.CreateAsync(new Models.Feel
                    {
                        UserId = userId,
                        ArticleId = articleId,
                        FeelType = Enumeration.FeelEnum.Good
                    });
                }

            }
        }

        /// <summary>
        /// 反对
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public async Task BadCount(Guid articleId, Guid userId)
        {
            //点赞表取消
            using (IDAL.IFeelService feelService = new DAL.FeelService())
            {
                var temp = feelService.GetAllAsync().FirstOrDefault(arg => arg.ArticleId == articleId && arg.UserId == userId);
                if (temp != null)
                {
                    if (temp.FeelType == Enumeration.FeelEnum.Good)
                    {
                        //如果此时的点赞的状态先取消点赞 第二次点击反对时才是反对
                        temp.FeelType = Enumeration.FeelEnum.None;
                        await feelService.EditAsync(temp);
                    }
                    else
                    {
                        temp.FeelType = Enumeration.FeelEnum.Bad;
                        await feelService.EditAsync(temp);
                    }
                }
                else
                {
                    await feelService.CreateAsync(new Models.Feel
                    {
                        UserId = userId,
                        ArticleId = articleId,
                        FeelType = Enumeration.FeelEnum.Bad
                    });
                }
            }
        }

        /// <summary>
        /// 不表态 这个是取消赞成和取消反对的时候触发的
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task NoAttitude(Guid articleId, Guid userId)
        {
            //点赞表取消
            using (IDAL.IFeelService feelService = new DAL.FeelService())
            {
                var temp = feelService.GetAllAsync().FirstOrDefault(arg => arg.ArticleId == articleId && arg.UserId == userId);
                if (temp != null)
                {
                    temp.FeelType = Enumeration.FeelEnum.None;
                    await feelService.EditAsync(temp);
                }
            }
        }

        /// <summary>
        /// 获得用户对这篇文章的态度
        /// </summary>
        /// <returns></returns>
        public Enumeration.FeelEnum? GetUserFeelToArticle(Guid articleId, Guid userId)
        {
            using (IDAL.IFeelService feelService = new DAL.FeelService())
            {
                var temp = feelService.GetAllAsync().FirstOrDefault(arg => arg.ArticleId == articleId && arg.UserId == userId);
                if (temp != null)
                {
                    return temp.FeelType;
                }
                else
                {
                    return Enumeration.FeelEnum.None;
                }
            }
        }



        #endregion

        #region 查看文章
        /// <summary>
        /// 阅读文章
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public async Task Read(Guid articleId, Guid userId)
        {
            using (IDAL.IReadingInfoService readingInfoService = new DAL.ReadingInfoService())
            {
                await readingInfoService.CreateAsync(new Models.ReadingInfo
                {
                    UserId = userId,
                    ArticleId = articleId
                });
            }
        }

        #endregion
    }
}
