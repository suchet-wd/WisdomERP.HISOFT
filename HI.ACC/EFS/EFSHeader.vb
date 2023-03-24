
Public Class EFSJSON
    Property request As EFSHeader = Nothing
End Class

Public Class EFSHeader
    Property manufacturing_invoice_number As String = ""
    Property factory_commercial_invoice_number As String = ""
    Property factory_code As String = ""

    Property total_invoice_amount As Decimal = 0.0

    Property invoice_currency_cd As String = ""
    Property worksheet_indicator As String = ""

    Property purchase_orders As List(Of EFS_PurchaseOrder) = Nothing
End Class

Public Class EFS_PurchaseOrder
    Property po_number As String = ""
    Property line_items As List(Of EFS_item_Line) = Nothing
End Class
Public Class EFS_item_Line
    Property product_cd As String = ""
    Property item_seq_number As Integer = 0
    Property included_sizes As List(Of EFS_item_sizes) = Nothing

End Class

Public Class EFS_item_sizes
    Property size As String = ""
    Property size_quantity As Integer = 0
    Property extended_first_sale_price As EFS_sale_price = Nothing

End Class

Public Class EFS_sale_price
    Property value As Decimal = 0.00
    Property currency_cd As String = ""

End Class

Public Class RefreshTokenResultJSON
    Public Property access_token As String
    Public Property token_type As String
    Public Property expires_in As Integer
    Public Property scope As String

End Class
