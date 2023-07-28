using LPBlazorServer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Cosmos.Storage.Internal;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Radzen;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace LPBlazorServer.Services
{
    public partial class DBService
    {
        LPBlazorServerContext Context
        {
            get
            {
                return this.context;
            }
        }

        private readonly LPBlazorServerContext context;
        private readonly NavigationManager navigationManager;

        public DBService(LPBlazorServerContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public void ApplyQuery<T>(ref IQueryable<T> items, Query query = null)
        {
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
        }

        partial void OnTransactionsRead(ref IQueryable<Transaction> items);

        public async Task<IQueryable<Transaction>> GetTransactions(Query query = null)
        {
            var items = Context.Transactions.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach (var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnTransactionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTransactionGet(Transaction item);
        partial void OnGetTransactionById(ref IQueryable<Transaction> items);

        public async Task<Transaction> OnGetTransactionById(string id)
        {
            var items = Context.Transactions
                              .AsNoTracking()
                              .Where(i => i.TransId == id);


            OnGetTransactionById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTransactionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTransactionUpdated(Transaction item);
        partial void OnAfterTransactionUpdated(Transaction item);

        public async Task<Transaction> UpdatetblProduct(string id, Transaction transaction)
        {
            OnTransactionUpdated(transaction);

            var itemToUpdate = Context.Transactions
                              .Where(i => i.TransId == transaction.TransId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
                throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(transaction);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTransactionUpdated(transaction);

            return transaction;
        }

        //partial void OntblProductDeleted(RadzenBlazorServer.Models.XcifySampleTestDB.tblProduct item);
        //partial void OnAftertblProductDeleted(RadzenBlazorServer.Models.XcifySampleTestDB.tblProduct item);

        //public async Task<RadzenBlazorServer.Models.XcifySampleTestDB.tblProduct> DeletetblProduct(int id)
        //{
        //    var itemToDelete = Context.tblProducts
        //                      .Where(i => i.Id == id)
        //                      .FirstOrDefault();

        //    if (itemToDelete == null)
        //    {
        //        throw new Exception("Item no longer available");
        //    }

        //    OntblProductDeleted(itemToDelete);


        //    Context.tblProducts.Remove(itemToDelete);

        //    try
        //    {
        //        Context.SaveChanges();
        //    }
        //    catch
        //    {
        //        Context.Entry(itemToDelete).State = EntityState.Unchanged;
        //        throw;
        //    }

        //    OnAftertblProductDeleted(itemToDelete);

        //    return itemToDelete;
        //}
    }
}
