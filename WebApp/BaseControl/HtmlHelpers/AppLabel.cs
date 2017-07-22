using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Text;

namespace BaseControl.HtmlHelpers
{
    public static class AppLabel
    {

        public static MvcHtmlString AppLabelView(this HtmlHelper htmlHelper, string id, string txt, string pageId, string styleTage = "Common")
        {
            id = id + pageId;
            TagBuilder tg = new TagBuilder("label");
            tg.MergeAttribute("class", styleTage);
            tg.GenerateId(id);
            tg.InnerHtml = txt;
            return MvcHtmlString.Create(tg.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Label控件，用于显示字段前的标题
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="pageId"></param>
        /// <param name="styleTage">对label设置样式,为空时默认为.labelCommon样式</param>
        /// <returns></returns>
        public static MvcHtmlString AppLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string pageId, string styleTage = "Common")
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            string id = name + pageId + "Label";
            TagBuilder tg = new TagBuilder("label");
            tg.MergeAttribute("for", name);
            tg.MergeAttribute("class", "label" + styleTage);
            tg.GenerateId(id);
            ModelMetadata metaproperty = ModelMetadataProviders.Current.GetMetadataForProperty(null, typeof(TModel), name);
            tg.InnerHtml = metaproperty.GetDisplayName();
            return MvcHtmlString.Create(tg.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString AppRequiredFlag(this HtmlHelper htmlHelper)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class='RequiredFlag'>*</div>");
            return MvcHtmlString.Create(sb.ToString());
        }

    }
}