using System;
using System.Windows.Forms;

namespace DTO
{
    public class DataGridViewNumericUpDownColumn : DataGridViewColumn
    {
        public DataGridViewNumericUpDownColumn()
            : base(new DataGridViewNumericUpDownCell())
        {
        }

        public decimal Minimum { get; set; }
        public decimal Maximum { get; set; }
        public decimal Increment { get; set; } = 1;
    }

    public class DataGridViewNumericUpDownCell : DataGridViewTextBoxCell
    {
        public DataGridViewNumericUpDownCell() : base()
        {
            this.Style.Format = "N0";
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue,
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            NumericUpDownEditingControl ctl = DataGridView.EditingControl as NumericUpDownEditingControl;

            if (this.OwningColumn is DataGridViewNumericUpDownColumn col)
            {
                ctl.Minimum = col.Minimum;
                ctl.Maximum = col.Maximum;
                ctl.Increment = col.Increment;
            }

            ctl.Value = Convert.ToDecimal(this.Value ?? ctl.Minimum);
        }

        public override Type EditType => typeof(NumericUpDownEditingControl);
        public override Type ValueType => typeof(decimal);
        public override object DefaultNewRowValue => 0m;
    }

    public class NumericUpDownEditingControl : NumericUpDown, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;

        public object EditingControlFormattedValue
        {
            get => Value.ToString("N0");
            set
            {
                if (decimal.TryParse(value?.ToString(), out decimal result))
                    Value = result;
            }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context) => EditingControlFormattedValue;
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle) { }
        public int EditingControlRowIndex { get => rowIndex; set => rowIndex = value; }
        public bool EditingControlValueChanged { get => valueChanged; set => valueChanged = value; }
        public Cursor EditingPanelCursor => base.Cursor;
        public bool RepositionEditingControlOnValueChange => false;

        public DataGridView EditingControlDataGridView
        {
            get => dataGridView;
            set => dataGridView = value;
        }

        protected override void OnValueChanged(EventArgs e)
        {
            valueChanged = true;
            this.dataGridView?.NotifyCurrentCellDirty(true);
            base.OnValueChanged(e);
        }

        public void PrepareEditingControlForEdit(bool selectAll) { }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            // Giúp người dùng có thể dùng phím mũi tên để tăng giảm giá trị
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }
    }
}