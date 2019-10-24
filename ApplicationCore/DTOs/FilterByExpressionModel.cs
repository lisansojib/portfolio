using System.Collections.Generic;

namespace ApplicationCore.DTOs
{
    /// <summary>
    /// FilterByExpressionModel
    /// </summary>
    public class FilterByExpressionModel
    {
        /// <summary>
        /// 
        /// </summary>
        public FilterByExpressionModel()
        {
            Parameters = new List<string>();
        }

        /// <summary>
        /// Expression
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// Parameters
        /// </summary>
        public List<string> Parameters { get; set; }
    }
}
