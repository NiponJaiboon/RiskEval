using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class PropertyValueGroupContainer : PropertyValueContainerBase
    {
        public PropertyValueGroupContainer()
        {
        }

        public PropertyValueGroupContainer(PropertyTemplateGroup templateGroup)
            : base(templateGroup)
        {
            //PropertyValueGroup valueGroup = new PropertyValueGroup(this);
            //foreach (PropertyTemplateBase template in templateGroup.Children)
            //{
            //    this.Children.Add(template.CreateValueContainer());
            //}
            //this.Add(valueGroup);
        }

        #region persistent

        private IList<PropertyValueContainerBase> children = new List<PropertyValueContainerBase>();
        public virtual IList<PropertyValueContainerBase> Children
        {
            get
            {
                if (children == null)
                    children = new List<PropertyValueContainerBase>();
                return children;
            }
        }

        #endregion persistent

        public virtual new PropertyTemplateGroup Template
        {
            get { return (PropertyTemplateGroup)base.template; }
            set { base.template = value; }
        }

        public virtual bool Add(PropertyValueGroupContainer group)
        {
            if (this.children == null)
                this.children = new List<PropertyValueContainerBase>();
            if (base.Template.Multiplicity > 0
                && this.children.Count >= base.Template.Multiplicity)
                return false;
            this.children.Add(group);
            return true;
        }

        //public virtual void RemoveValueGroup(PropertyValueGroup child)
        //{
        //    this.Values.Remove(child);
        //}
        public virtual void AddValue()
        {
        }

        public override void Save(Context context)
        {
            foreach (PropertyValueContainerBase valueGroup in this.Children)
            {
                valueGroup.Parent = this;
                valueGroup.Save(context);
            }
            context.Persist(this);
        }

    }
}
