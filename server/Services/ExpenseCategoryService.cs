using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IExpenseCategoryService
    {
        IEnumerable<ExpenseCategory> GetAll();
        ExpenseCategory GetById(int id);
        ExpenseCategory Create(ExpenseCategory expenseCategory);
        void Update(ExpenseCategory expenseCategory);
        void Delete(int id);
    }

    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private DataContext _db;

        public ExpenseCategoryService(DataContext dbContext)
        {
            _db = dbContext;
        }

        public ExpenseCategory Create(ExpenseCategory expenseCategory)
        {
            _db.ExpenseCategories.Add(expenseCategory);
            _db.SaveChanges();
            return expenseCategory;
        }

        public void Delete(int id)
        {
            var expenseCategoryToDelete = GetById(id);

            _db.ExpenseCategories.Remove(expenseCategoryToDelete);
            _db.SaveChanges();
        }

        public IEnumerable<ExpenseCategory> GetAll()
        {
            return _db.ExpenseCategories;
        }

        public ExpenseCategory GetById(int id)
        {
            var found = _db.ExpenseCategories.Find(id);
            if (found != null)
                return found;
            throw new AppException("Entity does not exist.");
        }

        public void Update(ExpenseCategory input)
        {
            var found = GetById(input.Id);
            found.Name = input.Name;
            _db.ExpenseCategories.Update(found);
            _db.SaveChanges();
        }
    }
}