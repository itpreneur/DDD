﻿using Conditions;
using System.Collections.Generic;

namespace DDD.Common.Domain
{
    using Core.Domain;

    /// <remarks>
    /// Example of overriding HashCodeComponents(), sacrificing uniqueness of hash codes for speed.
    /// </remarks>
    public class BinaryContent : ValueObject
    {

        #region Fields

        private static readonly BinaryContent emptyContent = new BinaryContent(new byte[0]);

        #endregion Fields

        #region Constructors

        public BinaryContent(byte[] content)
        {
            Condition.Requires(content, nameof(content)).IsNotNull();
            this.Content = content;
        }

        #endregion Constructors

        #region Properties

        public byte[] Content { get; }

        #endregion Properties

        #region Methods

        public static BinaryContent Empty() => emptyContent;

        public override IEnumerable<object> EqualityComponents()
        {
            foreach (var component in this.Content)
                yield return component;
        }

        /// <remarks>
        /// The hash code is computed on the last eight bytes of the array as in the .NET implementation of Array.IStructuralEquatable.GetHashCode().
        /// </remarks>
        public override IEnumerable<object> HashCodeComponents()
        {
            var startIndex = this.Content.Length > 8 ? this.Content.Length - 8 : 0;
            for (int i = startIndex; i < this.Content.Length; i++)
                yield return this.Content[i];
        }

        #endregion Methods

    }
}
