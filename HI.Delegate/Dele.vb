Public Class Dele

    Public Delegate Sub FNHSysEmpID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Delegate Sub FNHSysEmpTypeId_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Delegate Sub DynamicButtonedit_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Delegate Sub ButtonEdit_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Delegate Sub XtraTab_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs)
    Public Delegate Sub ButtonEdit_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
    Public Delegate Sub ButtonEdit_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs)
    Public Delegate Sub ButtonEdit_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs)
    Public Delegate Sub ButtonEdit_Spin(sender As Object, e As DevExpress.XtraEditors.Controls.SpinEventArgs)
    Public Delegate Sub ComboBoxEdit_SelectedIndexChange(ByVal sender As System.Object, ByVal e As System.EventArgs)

End Class
