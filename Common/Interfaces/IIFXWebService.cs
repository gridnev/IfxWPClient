using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Interfaces
{
    public interface IIFXWebService
    {
        event EventHandler<MyEventArgs> FreeNewsListCompleted;
        void FreeNewsListAsync(DateTime updatemark);

        event EventHandler<MyEventArgs> GetNewsByIdCompleted;
        void GetNewsByIdAsync(string newsId);

        event EventHandler<MyEventArgs> FreeArticleListCompleted;
        void FreeArticleListAsync(DateTime updatemark);
    }
}
