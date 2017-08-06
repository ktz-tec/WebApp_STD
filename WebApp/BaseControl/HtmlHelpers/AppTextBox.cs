using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Text;
using BaseCommon.Data;

namespace BaseControl.HtmlHelpers
{
    public static class AppTextBox
    {
        /// <summary>
        /// 文本输入控件
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="pageId"></param>
        /// <param name="styleTage">对text设置样式,为空时默认为.textCommon样式</param>
        /// <param name="textType">控件类型</param>
        /// <returns></returns>
        public static MvcHtmlString AppTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string pageId, string styleTage = "Common", TextType textType = TextType.Text)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            string id = name + pageId ;
            object data = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData).Model;

            TagBuilder tg = new TagBuilder("input");
            tg.MergeAttribute("name", name, true);
            tg.MergeAttribute("class", "text" + styleTage);
            tg.GenerateId(id);

            if (data != null)
            {
                tg.MergeAttribute("value", data.ToString());
            }

            if (textType == TextType.Password)
            {
                tg.MergeAttribute("type", "password");
            }
            else if (textType == TextType.Hidden)
            {
                tg.MergeAttribute("type", "hidden");
            }
            else
            {
                tg.MergeAttribute("type", "text");
            }
            return MvcHtmlString.Create(tg.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString AppHiddenFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string pageId)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            string id = name + pageId;
            object data = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData).Model;

            TagBuilder tg = new TagBuilder("input");
            tg.MergeAttribute("name", name, true);
            tg.GenerateId(id);

            if (data != null)
            {
                tg.MergeAttribute("value", data.ToString());
            }

            tg.MergeAttribute("type", "hidden");

            return MvcHtmlString.Create(tg.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString AppTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string pageId, int rows, int cols, string styleTage = "Common")
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            string id = name + pageId;
            object data = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData).Model;

            TagBuilder tg = new TagBuilder("textarea");
            tg.MergeAttribute("name", name, true);
            tg.MergeAttribute("class", "textarea" + styleTage);
            tg.MergeAttribute("rows", rows.ToString());
            if (cols != 0)
                tg.MergeAttribute("cols", cols.ToString());
            tg.GenerateId(id);

            StringBuilder sb = new StringBuilder();

            if (data != null)
            {
                sb.AppendLine("<script type=\"text/javascript\">");
                sb.AppendLine("$(document).ready(function () {");
                sb.AppendLine(string.Format("$('#{0}').val('{1}'); ", id, data));
                sb.AppendLine("});");
                sb.AppendLine("</script>");
            }

            return MvcHtmlString.Create(tg.ToString(TagRenderMode.Normal) + sb.ToString());
        }

    }
}