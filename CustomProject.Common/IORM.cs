using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Common
{
    public interface IORM<T> where T:class
    {
        Result<List<T>> Select();
        Result<bool> Insert(T entity);
        Result<bool> Update(T entity);
        Result<bool> Delete(T entity);
        Result<List<T>> SelectActive();
        Result<List<T>> SelectById(int id);
        Result<List<T>> SelectForeignById(int? id);
        Result<List<T>> SelectForeignByIdCom(int? id);
        Result<List<T>> OrderByDesc(int count);
        Result<List<T>> OrderByAsc(int count);
        Result<List<T>> Contain(string columnName, string word);
    }
}