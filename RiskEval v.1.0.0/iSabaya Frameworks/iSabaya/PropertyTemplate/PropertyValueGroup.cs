using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class PropertyValueGroup
    {
        public PropertyValueGroup()
        {
        }

        public PropertyValueGroup(PropertyValueGroupContainer parent)
        {
            this.parent = parent;
        }

        #region persistent

        private int propertyValueGroupID;
        public virtual int PropertyValueGroupID
        {
            get { return propertyValueGroupID; }
            set { propertyValueGroupID = value; }
        }

        private PropertyValueGroupContainer parent;
        public virtual PropertyValueGroupContainer Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        private IList<PropertyValueContainerBase> children;
        public virtual IList<PropertyValueContainerBase> Children
        {
            get
            {
                if (children == null)
                    children = new List<PropertyValueContainerBase>();
                return this.children;
            }
            set
            {
                this.children = value;
            }
        }

        #endregion persistent

        public virtual bool AddChild(PropertyValueContainerBase child)
        {
            this.Children.Add(child);
            return true;
        }

        public virtual void RemoveChild(PropertyValueContainerBase child)
        {
            this.Children.Remove(child);
        }

        public virtual void Save(Context context)
        {
            foreach (PropertyValueContainerBase value in children)
            {
                value.Save(context);
            }
            context.Persist(this);
        }
    }
}
