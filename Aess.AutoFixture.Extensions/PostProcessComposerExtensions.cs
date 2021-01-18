using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoFixture;
using AutoFixture.Dsl;

namespace Aess.AutoFixture.Extensions
{
    public static class PostProcessComposerExtensions{

        public static IEnumerable<IPostprocessComposer<T>> With<T, TPropType>(this IEnumerable<IPostprocessComposer<T>> items,
            Expression<Func<T, TPropType>> selector, TPropType value)
        {
            return items.Select(r => r.With(selector, value));
        }
        
        public static IEnumerable<IPostprocessComposer<T>> With<T, TPropType>(this IEnumerable<IPostprocessComposer<T>> items, Expression<Func<T, TPropType>> selector, Func<TPropType> valueFunc)
        {
            return items.Select(r => r.With(selector, valueFunc));
        }
        
        public static IEnumerable<IPostprocessComposer<T>> Without<T, TPropType>(this IEnumerable<IPostprocessComposer<T>> items,
            Expression<Func<T, TPropType>> selector)
        {
            return items.Select(r => r.Without(selector));
        }

        public static IEnumerable<T> Create<T>(this IEnumerable<IPostprocessComposer<T>> items)
        {
            return items.Select(r => r.Create());
        }

        public static IEnumerable<IPostprocessComposer<T>> WithAutoProperties<T>(
            this IEnumerable<IPostprocessComposer<T>> items)
        {
            return items.Select(r => r.WithAutoProperties());
        }

        public static IEnumerable<IPostprocessComposer<T>> OmitAutoProperties<T>(
            this IEnumerable<IPostprocessComposer<T>> items)
        {
            return items.Select(r => r.OmitAutoProperties());
        }
    }
}