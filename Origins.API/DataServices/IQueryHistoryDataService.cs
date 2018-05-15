
using Origins.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API.DataServices
{
    public interface IQueryHistoryDataService : ICrudDataService<QueryHistoryModel, string>
    {
        /// <summary>
        /// Add a history. Id and date will be automatically completed.
        /// </summary>
        /// <param name="incompleteModel"></param>
        /// <returns></returns>
        Task AddHistoryAsync(QueryHistoryModel incompleteModel);

        /// <summary>
        /// Deletes query history of username and productId
        /// </summary>
        /// <param name="username"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task DeleteHistoryAsync(string username, string productId);
    }
}
