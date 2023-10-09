
using Dapper;
using infrastructure.Model;
using Npgsql;

namespace infrastructure;

public class Reposotory
{
    private readonly NpgsqlDataSource _dataSource;

    public Reposotory(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<Article> getAllArticles()
    {
        var sql = $@"
SELECT articleid, headline, SUBSTRING(body, 1, 50) AS body, author, articleimgurl FROM news.articles;
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Article>(sql);
        }
    }

    public Article createArticle(string headline, string body, string author, string articleImgUrl)
    {
        var sql = $@"
            INSERT INTO news.articles (headline, body, author, articleimgurl) 
            VALUES (@headline, @body, @author, @articleimgurl)
              RETURNING 
            articleid as {nameof(Article.ArticleId)}, 
            headline as {nameof(Article.Headline)},
            body as {nameof(Article.Body)} ,
            author as {nameof(Article.Author)},
            articleimgurl as {nameof(Article.ArticleImgUrl)};";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QuerySingle<Article>(sql, new {headline,  body,  author,  articleImgUrl});
        }
    }

    public void deleteArticle(int id)
    {
        var sql = $@"
            DELETE FROM news.articles where articleid = @articleId";
        using (var conn = _dataSource.OpenConnection())
        {
            conn.Execute(sql, new {articleId = id});
        }
    }

    public Article getFullArticle(int articleId)
    {
        var sql = $@" SELECT * FROM news.articles WHERE articleid = @articleId;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Article>(sql, new {articleId = articleId});
        }
    }

    public IEnumerable<SearchArticleItem> searchForArticles(string searchterm, int pageSize)
    {
        var sql = $@"
         SELECT * FROM news.articles 
         WHERE headline LIKE '%' || @searchterm || '%' OR body LIKE '%' || @searchterm || '%' LIMIT @pageSize;
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<SearchArticleItem>(sql, new {searchterm, pageSize});
        }
    }

    public Article updateArticleById(int articleId, Article article)
    {
        var sql = $@"UPDATE news.articles
                    SET headline = @headline, body= @body, author = @author, articleimgurl = @articleimgurl
                    WHERE articleid = @articleId
                    RETURNING *;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Article>(sql,new {headline = article.Headline,  body = article.Body,  author = article.Author,  articleImgUrl = article.ArticleImgUrl, articleId = articleId});
        }
    }

    public IEnumerable<Article> searchForArticlesReturnArticle(string searchterm, int pageSize)
    {
        var sql = $@"
         SELECT * FROM news.articles 
         WHERE headline LIKE '%' || @searchterm || '%' OR body LIKE '%' || @searchterm || '%' LIMIT @pageSize;
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Article>(sql, new {searchterm, pageSize});
        }
    }
}
