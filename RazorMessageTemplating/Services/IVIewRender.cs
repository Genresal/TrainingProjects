public interface IViewRender
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="name"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    string RenderPartialViewToString<TModel>(string name, TModel model);
}