using ApplicationCore.DTOs;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Web.Utilities
{
    public interface ICommonHelpers
    {
        string GetFilterBy(string value);
        FilterByExpressionModel GetFilterByModel(string value);
    }

    public class CommonHelpers : ICommonHelpers
    {
        public string GetFilterBy(string value)
        {
            var filterBy = string.Empty;
            if (!string.IsNullOrEmpty(value))
            {
                var singleFilter = true;
                dynamic filterObj = JsonConvert.DeserializeObject(value);
                foreach (var item in filterObj)
                {
                    var appendAnd = singleFilter ? "" : " And ";
                    filterBy += $"{appendAnd}{item.Name} like '%{item.Value.Value}%'";
                    singleFilter = false;
                }
            }

            return filterBy;
        }

        public FilterByExpressionModel GetFilterByModel(string value)
        {
            FilterByExpressionModel filterExpressionModel = null;

            if (!string.IsNullOrEmpty(value))
            {
                filterExpressionModel = new FilterByExpressionModel();
                StringBuilder expression = null;
                var singleFilter = true;
                dynamic filterObj = JsonConvert.DeserializeObject(value);
                int i = 0;
                foreach (var item in filterObj)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(item.Value)))
                        continue;

                    var appendAnd = singleFilter ? "" : " AND ";
                    expression = new StringBuilder();
                    expression.Append($"{appendAnd}{item.Name}.ToString().Contains(@{i++})");
                    singleFilter = false;

                    filterExpressionModel.Parameters.Add(Convert.ToString(item.Value));
                }

                filterExpressionModel.Expression = expression.ToString();
            }

            return filterExpressionModel;
        }
    }
}
