using System;
using System.Collections.Generic;
using System.Linq;

namespace DGames.Presets
{
    public class BinderBase
    {

        [NonSerialized]protected readonly List<BindSettings> binds = new();
        
        protected void Bind(object raised, object binding)
        {
            if (binds.Any(b=>b.binding == binding))
            {
                throw new InvalidOperationException();
            }

            binds.Add(new BindSettings
            {
                binding = binding,
                raised = raised
            });
        }

        protected void Bind(object raised, string id)
        {
            if (binds.Any(b=>b.id == id))
            {
                throw new InvalidOperationException();
            }
            
            binds.Add(new BindSettings
            {
                id = id,
                raised = raised
            });

           
        }

        public void UnBind(object binding)
        {
            binds.RemoveAll(b => b.binding == binding);
        }

        public void UnBind(string id)
        {
            binds.RemoveAll(b => b.id == id);
        }

        public bool HasBinding(object binding) => binds.Any(b => b.binding == binding);
        public bool HasBinding(string id) => binds.Any(b => b.id == id);

        public void Clear()
        {
            binds.Clear();
        }

        public void ClearEmpty()
        {
            binds.RemoveAll(b => b.binding == null && string.IsNullOrEmpty(b.id));
        }

        public struct BindSettings
        {
            public object binding;
            public object raised;
            public string id;
        }
    }
    
    public class Binder<T,TJ,TJj> : BinderBase
    {
        public void Raised(T valueOne,TJ valueTwo,TJj valueThree)
        {
            binds.Where(b=>b.raised!=null).Select(b=>(Action<T,TJ,TJj>)b.raised).ForEach(c=>c(valueOne,valueTwo,valueThree));
        }

        public void Bind(Action<T,TJ,TJj> raised, object binding) => Bind(raised as object,binding);
        public void Bind(Action<T,TJ,TJj> raised, string id) => Bind(raised as object,id);
    }

    public class Binder<T, TJ> : BinderBase
    {
        public void Raised(T valueOne, TJ valueTwo)
        {
            binds.Where(b => b.raised != null).Select(b => (Action<T, TJ>)b.raised).ForEach(c => c(valueOne, valueTwo));
        }

        public void Bind(Action<T, TJ> raised, object binding) => Bind(raised as object, binding);
        public void Bind(Action<T, TJ> raised, string id) => Bind(raised as object, id);
    }

    public class Binder<T> : BinderBase
    {
        public void Raised(T value)
        {
            binds.Where(b => b.raised != null).Select(b => (Action<T>)b.raised).ForEach(c => c(value));
        }

        public void Bind(Action<T> raised, object binding) => Bind(raised as object, binding);
        public void Bind(Action<T> raised, string id) => Bind(raised as object, id);
    }


    public static class LinqExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }

}