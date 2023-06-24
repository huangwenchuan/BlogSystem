using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.WebControls.Mvc;

namespace BlogSyStem.IBLL
{
    public interface IArticleManager
    {
        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="categoryIds"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task CreateArticle(string title, string content, Guid[] categoryIds,Guid userId);

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        Task RemoveArticle(Guid articleId);

        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="categoryIds"></param>
        /// <returns></returns>
        Task EditArticle(Guid ArticleId, string title, string content, Guid[] categoryIds);

        /// <summary>
        /// 创建类别
        /// </summary>
        /// <param name="name"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task CreateCategory(string name,Guid userId);

        /// <summary>
        /// 删除类别
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task RemoveCategory(Guid categoryId);

        /// <summary>
        /// 编辑类别
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="newcategoryName"></param>
        /// <returns></returns>
        Task EditCategory(Guid categoryId,string newcategoryName);

        Task<bool> ExistsArticle(Guid articleId);

        Task<Dto.ArticleDto> GetArticleModelById(Guid articleId);

        /// <summary>
        /// 根据用户Id获取所有分类
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Dto.CategoryDto>> GetAllCategories(Guid userId);

        /// <summary>
        /// 根据用户Id获取所有文章
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Dto.ArticleDto>> GetAllArticlesByUserId(Guid userId, int pageIndex, int pageSize);

        Task<List<Dto.ArticleDto>> GetAllArticlesByUserId(Guid userId);

        /// <summary>
        /// 根据用户邮件获取所有文章
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<List<Dto.ArticleDto>> GetAllArticlesByUserEmail(string email);

        /// <summary>
        /// 根据类别Id获取所有文章
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<List<Dto.ArticleDto>> GetAllArticlesByCategoryId(Guid categoryId);

        /// <summary>
        /// 获取用户文章总条数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> GetDataCount(Guid userId);

        /// <summary>
        /// 赞
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns></returns>
        Task GoodCount(Guid articleId);

        /// <summary>
        /// 踩
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns></returns>
        Task BadCount(Guid articleId);

        /// <summary>
        /// 赞
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns></returns>
        Task GoodCount(Guid articleId,Guid userId);

        /// <summary>
        /// 踩
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns></returns>
        Task BadCount(Guid articleId, Guid userId);

        /// <summary>
        /// 创建评论
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="articleId">文章Id</param>
        /// <param name="content">评论内容</param>
        /// <returns></returns>
        Task CreateComment(Guid userId, Guid articleId,string content);

        /// <summary>
        /// 根据文章Id获取评论对象集合
        /// </summary>
        /// <param name="articleId">文章Id</param>
        /// <returns></returns>
        Task<List<Dto.CommentDto>> GetCommentModelListByArticleId(Guid articleId);

        Task NoAttitude(Guid articleId,Guid userId);

        Task<List<Dto.ArticleDto>> GetAllArticles(Guid userId);

        Task Read(Guid articleId, Guid userId);
    }
}
