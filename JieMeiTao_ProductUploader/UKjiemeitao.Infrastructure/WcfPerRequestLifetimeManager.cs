using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Microsoft.Practices.Unity;

namespace UKjiemeitao.Infrastructure
{
    /// <summary>
    /// 表示一个基于字典的对象列表，该列表中的对象生命周期将由WCF的操作上下文托管。
    /// </summary>
    internal class InstanceItems
    {
        #region Private Fields
        private readonly Dictionary<object, object> items = new Dictionary<object, object>();
        #endregion

        #region Private Methods
        private void CleanUp(object sender, EventArgs e)
        {
            foreach (var item in items.Select(item => item.Value))
            {
                if (item is IDisposable)
                    ((IDisposable)item).Dispose();
            }
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Finds an item by the given key.
        /// </summary>
        /// <param name="key">The key of the item.</param>
        /// <returns>The instance of the item.</returns>
        internal object Find(object key)
        {
            if (items.ContainsKey(key))
                return items[key];
            return null;
        }
        /// <summary>
        /// Sets an item with the given key.
        /// </summary>
        /// <param name="key">The key of the item.</param>
        /// <param name="value">The instance of the item.</param>
        internal void Set(object key, object value)
        {
            items[key] = value;
        }
        /// <summary>
        /// Removes the item with the given key.
        /// </summary>
        /// <param name="key">The key of the item to be removed.</param>
        internal void Remove(object key)
        {
            items.Remove(key);
        }

        /// <summary>
        /// Hooks the Closed and Faulted events on the <see cref="InstanceContext"/> object.
        /// </summary>
        /// <param name="instanceContext">The <see cref="InstanceContext"/> object whose
        /// Closed and Faulted events are hooked.</param>
        internal void Hook(InstanceContext instanceContext)
        {
            instanceContext.Closed += CleanUp;
            instanceContext.Faulted += CleanUp;
        }
        #endregion
    }

    internal class WcfServiceInstanceExtension : IExtension<InstanceContext>
    {
        #region Private Fields
        private readonly InstanceItems items = new InstanceItems();
        #endregion

        #region Internal Properties
        /// <summary>
        /// Gets the instance of <see cref="InstanceItems"/> object.
        /// </summary>
        internal InstanceItems Items { get { return items; } }
        #endregion

        #region Internal Static Properties
        /// <summary>
        /// Gets the current instance of <see cref="WcfServiceInstanceExtension"/> class.
        /// </summary>
        internal static WcfServiceInstanceExtension Current
        {
            get
            {
                if (OperationContext.Current == null)
                    return null;
                var instanceContext = OperationContext.Current.InstanceContext;
                return GetExtensionFrom(instanceContext);
            }
        }
        #endregion

        #region Private Methods
        private static WcfServiceInstanceExtension GetExtensionFrom(InstanceContext instanceContext)
        {
            lock (instanceContext)
            {
                WcfServiceInstanceExtension extension = instanceContext.Extensions.Find<WcfServiceInstanceExtension>();
                if (extension == null)
                {
                    extension = new WcfServiceInstanceExtension();
                    extension.Items.Hook(instanceContext);
                    instanceContext.Extensions.Add(extension);
                }
                return extension;
            }
        }
        #endregion

        #region IExtension<InstanceContext> Members
        /// <summary>
        /// Enables an extension object to find out when it has been aggregated. Called
        /// when the extension is added to the <see cref="System.ServiceModel.IExtensibleObject&lt;T&gt;.Extensions"/>
        /// property.
        /// </summary>
        /// <param name="owner">The extensible object that aggregates this extension.</param>
        public void Attach(InstanceContext owner) { }
        /// <summary>
        /// Enables an object to find out when it is no longer aggregated. Called when
        /// an extension is removed from the <see cref="System.ServiceModel.IExtensibleObject&lt;T&gt;.Extensions"/>
        /// property.
        /// </summary>
        /// <param name="owner">The extensible object that aggregates this extension.</param>
        public void Detach(InstanceContext owner) { }

        #endregion
    }

    public class WcfPerRequestLifetimeManager : LifetimeManager
    {
        #region Private Fields
        private readonly Guid key = Guid.NewGuid();
        #endregion

        #region Public Methods
        /// <summary>
        /// Retrieve a value from the backing store associated with this Lifetime policy.
        /// </summary>
        /// <returns>The object desired, or null if no such object is currently stored.</returns>
        public override object GetValue()
        {
            return WcfServiceInstanceExtension.Current.Items.Find(key);
        }
        /// <summary>
        /// Remove the given object from backing store.
        /// </summary>
        public override void RemoveValue()
        {
            WcfServiceInstanceExtension.Current.Items.Remove(key);
        }
        /// <summary>
        /// Stores the given value into backing store for retrieval later.
        /// </summary>
        /// <param name="newValue">The object being stored.</param>
        public override void SetValue(object newValue)
        {
            WcfServiceInstanceExtension.Current.Items.Set(key, newValue);
        }
        #endregion
    }
}
