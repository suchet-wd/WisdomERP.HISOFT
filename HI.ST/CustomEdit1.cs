using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Popup;

namespace HI.ST {
    [UserRepositoryItem("RegisterCustomLookupEdit")]
    public class RepositoryItemCustomLookupEdit : RepositoryItemLookUpEdit {
        static RepositoryItemCustomLookupEdit() {
            RegisterCustomLookupEdit();
        }

        public const string CustomEditName = "RepositoryItemCustomLookupEdit";

        public RepositoryItemCustomLookupEdit() {
        }

        public override string EditorTypeName {
            get {
                return CustomEditName;
            }
        }

        public static void RegisterCustomLookupEdit() {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(CustomLookupEdit), typeof(RepositoryItemCustomLookupEdit), typeof(CustomEdit1ViewInfo), new CustomEdit1Painter(), true, img));
        }

        public override void Assign(RepositoryItem item) {
            BeginUpdate();
            try {
                base.Assign(item);
                RepositoryItemCustomLookupEdit source = item as RepositoryItemCustomLookupEdit;
                if(source == null) return;
                //
            } finally {
                EndUpdate();
            }
        }
    }

    [ToolboxItem(true)]
    public class CustomLookupEdit : LookUpEdit {
        static CustomLookupEdit() {
            RepositoryItemCustomLookupEdit.RegisterCustomLookupEdit();
        }

        public CustomLookupEdit() {
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemCustomLookupEdit Properties {
            get {
                return base.Properties as RepositoryItemCustomLookupEdit;
            }
        }

        public override string EditorTypeName {
            get {
                return RepositoryItemCustomLookupEdit.CustomEditName;
            }
        }

        protected override int FindItem(string text, bool partialSearch, int startIndex) {
            //return -1;

            //if (text == null) return -1;
            //if (text.Length == 0 && partialSearch) return -1;
            //if (!Properties.CaseSensitiveSearch) text = text.ToLower();
            //if (startIndex < 0) startIndex = 0;
            //for (int i = startIndex; i < Properties.DataAdapter.ItemCount; i++)
            //{

             

            //    string itemText = Properties.GetValueDisplayText(Properties.ActiveFormat, Properties.DataAdapter.GetValueAtIndex(Properties.DisplayMember, i));
            //    if (!Properties.CaseSensitiveSearch) itemText = itemText.ToLower();
            //    if (partialSearch)
            //    {
            //        if (text == itemText.Substring(0, Math.Min(itemText.Length, text.Length))) return i;
            //    }
            //    else
            //    {
            //        if (text == itemText) return i;
            //    }
            //}
            return -1;

        }

        protected override PopupBaseForm CreatePopupForm() {
            return new CustomEdit1PopupForm(this);
        }
    }

    public class CustomEdit1ViewInfo : LookUpEditViewInfo {
        public CustomEdit1ViewInfo(RepositoryItem item)
            : base(item) {
        }
    }

    public class CustomEdit1Painter : ButtonEditPainter {
        public CustomEdit1Painter() {
        }
    }

    public class CustomEdit1PopupForm : PopupLookUpEditForm {
        public CustomEdit1PopupForm(CustomLookupEdit ownerEdit)
            : base(ownerEdit) {
        }
    }
}
