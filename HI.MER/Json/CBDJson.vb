
Public Class EFSCBDJSON
    Property request As CBDJson = Nothing
End Class

Public Class EFSCBDMULTIJSON
    Property request As CBDMultiJson = Nothing
End Class
Public Class CBDJson
    Property CBDID As String = ""
    Property season As String = ""
    Property factory_code As String = ""

    Property style_no As String = ""

    Property style_name As String = ""
    Property is_team_cbd As String = ""
    Property color As String = ""

    Property size As String = ""
    Property buy_type As String = ""
    Property version As Integer = 0
    Property msc As String = ""
    Property developer As String = ""
    Property cbd_quote_status As String = ""
    Property sample_round As String = ""
    Property base_style_no As String = ""
    Property quoted_date As Date = Nothing
    Property quote_log As String = ""
    Property comment As String = ""

    Property Tbl_Imp_FOBSummary As CBDJson_Tbl_Imp_FOBSummary = Nothing
    Property Tbl_Imp_CMP As List(Of CBDJson_Tbl_Imp_CMP) = Nothing
    Property Tbl_Imp_L4L1 As CBDJson_Tbl_Imp_L4L1 = Nothing
    Property Tbl_Imp_L4L2 As CBDJson_Tbl_Imp_L4L2 = Nothing
    Property Tbl_Imp_L4L3 As CBDJson_Tbl_Imp_L4L3 = Nothing
    Property Tbl_Imp_Fabric As List(Of CBDJson_Tbl_Imp_Fabric) = Nothing
    Property Tbl_Imp_Trims As List(Of CBDJson_Tbl_Imp_Trims) = Nothing
    Property Tbl_Imp_Process_Mtrl As List(Of CBDJson_Tbl_Imp_Process_Mtrl) = Nothing
    Property Tbl_Imp_Process_Labor As List(Of CBDJson_Tbl_Imp_Process_Labor) = Nothing
    Property Tbl_Imp_Packaging As List(Of CBDJson_Tbl_Imp_Packaging) = Nothing
    Property Tbl_Imp_BEMIS As List(Of CBDJson_Tbl_Imp_BEMIS) = Nothing

End Class


Public Class CBDMultiJson
    Property CBDID As String = ""
    Property season As String = ""
    Property factory_code As String = ""

    Property style_no As String = ""

    Property style_name As String = ""
    Property is_team_cbd As String = ""
    Property color As String = ""

    Property size As String = ""
    Property buy_type As String = ""
    Property version As Integer = 0
    Property msc As String = ""
    Property developer As String = ""
    Property cbd_quote_status As String = ""
    Property sample_round As String = ""
    Property base_style_no As String = ""
    Property quoted_date As Date = Nothing
    Property quote_log As String = ""
    Property comment As String = ""

    Property Tbl_Imp_FOBSummary As CBDJson_Tbl_Imp_FOBSummary = Nothing
    Property Tbl_Imp_CMP As List(Of CBDJson_Tbl_Imp_CMP) = Nothing
    Property Tbl_Imp_L4L1 As CBDJson_Tbl_Imp_L4L1 = Nothing
    Property Tbl_Imp_L4L2 As CBDJson_Tbl_Imp_L4L2 = Nothing
    Property Tbl_Imp_L4L3 As CBDJson_Tbl_Imp_L4L3 = Nothing
    Property Tbl_Imp_Fabric As List(Of CBDJson_Tbl_Imp_Fabric) = Nothing
    Property Tbl_Imp_Trims As List(Of CBDJson_Tbl_Imp_Trims) = Nothing
    Property Tbl_Imp_Process_Mtrl As List(Of CBDJson_Tbl_Imp_Process_Mtrl) = Nothing
    Property Tbl_Imp_Process_Labor As List(Of CBDJson_Tbl_Imp_Process_Labor) = Nothing
    Property Tbl_Imp_Packaging As List(Of CBDJson_Tbl_Imp_Packaging) = Nothing
    Property Tbl_Imp_BEMIS As List(Of CBDJson_Tbl_Imp_BEMIS) = Nothing

    Property ChildCbds As List(Of CBDJson_ChildCbds) = Nothing

End Class

Public Class CBDJson_Tbl_Imp_FOBSummary
    Property total_fabric As Decimal = 0.00
    Property total_trim As Decimal = 0.00
    Property charge_fabric As Decimal = 0.00
    Property charge_trim As Decimal = 0.00
    Property process_material_cost As Decimal = 0.00
    Property process_labor_cost As Decimal = 0.00
    Property packaging As Decimal = 0.00
    Property other_cost As Decimal = 0.00
    Property cmp As Decimal = 0.00
    Property extended_size_adjustment As Decimal = 0.00
    Property final_fob As Decimal = 0.00
    Property extended_size_fob As Decimal = 0.00
    Property trim_usage_allowance As Decimal = 0.00
End Class

Public Class CBDJson_Tbl_Imp_CMP
    Public Property bmccode As String = ""
    Public Property cost_per_minute As Decimal = 0.00
    Public Property standard_allowed_minute As Decimal = 0.00
    Public Property efficiency As Decimal = 0.00
    Public Property profit As Decimal = 0.00
    Public Property cmpcost As Decimal = 0.00

End Class
Public Class CBDJson_Tbl_Imp_L4L1
    Public Property country As String = ""
    Public Property currency As String = ""
    Public Property exchange_rate As Decimal = 0.00
    Public Property local_currency_fob As Decimal = 0.00
    Public Property extended_size_fob As Decimal = 0.00
End Class

Public Class CBDJson_Tbl_Imp_L4L2
    Public Property country As String = ""
    Public Property currency As String = ""
    Public Property exchange_rate As Decimal = 0.00
    Public Property local_currency_fob As Decimal = 0.00
    Public Property extended_size_fob As Decimal = 0.00

End Class

Public Class CBDJson_Tbl_Imp_L4L3
    Public Property country As String = ""
    Public Property currency As String = ""
    Public Property exchange_rate As Decimal = 0.00
    Public Property local_currency_fob As Decimal = 0.00
    Public Property extended_size_fob As Decimal = 0.00

End Class

Public Class CBDJson_Tbl_Imp_Fabric
    Public Property pps_item_number As String = ""
    Public Property material_color As String = ""
    Public Property description As String = ""
    Public Property vendor As String = ""
    Public Property country_origin As String = ""
    Public Property use_location As String = ""
    Public Property weight As Decimal = 0.00
    Public Property width As String = ""
    Public Property width_unit As String = ""
    Public Property marker_efficiency_percentage As Decimal = 0.00
    Public Property net_usage As Decimal = 0.00
    Public Property allowance_percentage As Decimal = 0.00
    Public Property gross_usage As Decimal = 0.00
    Public Property rmds_season As String = ""
    Public Property rmds_status As String = ""
    Public Property uom As String = ""
    Public Property unit_price As Decimal = 0.00
    Public Property cost_insurance_freight As Decimal = 0.00
    Public Property usage_cost As Decimal = 0.00
    Public Property handling_charge_percentage As Decimal = 0.00
    Public Property handling_charge_cost As Decimal = 0.00
    Public Property import_duty As Decimal = 0.00


End Class

Public Class CBDJson_Tbl_Imp_Trims
    Public Property pps_item_number As String = ""
    Public Property material_color As String = ""
    Public Property description As String = ""
    Public Property vendor As String = ""
    Public Property country_origin As String = ""
    Public Property use_location As String = ""
    Public Property width As String = ""
    Public Property width_unit As String = ""
    Public Property net_usage As Decimal = 0.00
    Public Property allowance_percentage As Decimal = 0.00
    Public Property gross_usage As Decimal = 0.00
    Public Property rmds_season As String = ""
    Public Property rmds_status As String = ""
    Public Property uom As String = ""
    Public Property unit_price As Decimal = 0.00
    Public Property cost_insurance_freight As Decimal = 0.00
    Public Property usage_cost As Decimal = 0.00
    Public Property handling_charge_percentage As Decimal = 0.00
    Public Property handling_charge_cost As Decimal = 0.00
    Public Property import_duty As Decimal = 0.00

End Class


Public Class CBDJson_Tbl_Imp_Process_Mtrl
    Public Property pps_item_number As String = ""
    Public Property process_subtype As String = ""
    Public Property description As String = ""
    Public Property vendor As String = ""
    Public Property country_origin As String = ""
    Public Property use_location As String = ""
    Public Property net_usage As Decimal = 0.00
    Public Property allowance_percentage As Decimal = 0.00
    Public Property gross_usage As Decimal = 0.00
    Public Property uom As String = ""
    Public Property unit_price As Decimal = 0.00
    Public Property usage_cost As Decimal = 0.00
    Public Property import_duty As Decimal = 0.00
End Class

Public Class CBDJson_Tbl_Imp_Process_Labor
    Public Property pps_item_number As String = ""
    Public Property process_subtype As String = ""
    Public Property description As String = ""
    Public Property vendor As String = ""
    Public Property country_origin As String = ""
    Public Property use_location As String = ""
    Public Property gross_usage As Decimal = 0.00
    Public Property uom As String = ""
    Public Property unit_price As Decimal = 0.00
    Public Property usage_cost As Decimal = 0.00
    Public Property import_duty As Decimal = 0.00
End Class


Public Class CBDJson_Tbl_Imp_Packaging
    Public Property pps_item_number As String = ""
    Public Property description As String = ""
    Public Property vendor As String = ""
    Public Property country_origin As String = ""

    Public Property use_location As String = ""
    Public Property width As String = ""
    Public Property width_unit As String = ""
    Public Property net_usage As Decimal = 0.00
    Public Property allowance_percentage As Decimal = 0.00
    Public Property gross_usage As Decimal = 0.00
    Public Property rmds_season As String = ""
    Public Property rmds_status As String = ""
    Public Property uom As String = ""
    Public Property unit_price As Decimal = 0.00
    Public Property cost_insurance_freight As Decimal = 0.00
    Public Property usage_cost As Decimal = 0.00
    Public Property handling_charge_percentage As Decimal = 0.00
    Public Property handling_charge_cost As Decimal = 0.00
    Public Property import_duty As Decimal = 0.00

End Class


Public Class CBDJson_Tbl_Imp_BEMIS
    Public Property pps_item_number As String = ""
    Public Property full_width As Decimal = 0.00
    Public Property slitting_width As Decimal = 0.00
    Public Property required_length As Decimal = 0.00
    Public Property usage_full_width As Decimal = 0.00
    Public Property price_meter As Decimal = 0.00
    Public Property bemis_slitting_percentage As Decimal = 0.00
    Public Property price_slitting_width As Decimal = 0.00

End Class

Public Class CBDJson_ChildCbds
    Property msc As String = ""
    Property season As String = ""
    Property style_no As String = ""
    Property style_name As String = ""
    Property color As String = ""
    Property embellishments As List(Of CBDJson_embellishments) = Nothing
    Property child_l4ls As List(Of CBDJson_child_l4ls) = Nothing
    Property developer As String = ""
    Property comment As String = ""

End Class

Public Class CBDJson_embellishments
    Property emb_pps_item_number As String = ""
    Property process_type As String = ""
    Property emb_description As String = ""

    Property emb_vendor As String = ""
    Property emb_unit_price As Decimal = 0.0
    Property emb_cost_insurance_freight As Decimal = 0
    Property emb_usage_cost As Decimal = 0.0

    Property emb_handling_percentage As Decimal = 0

    Property emb_handling_cost As Decimal = 0
    Property emb_total_trim_cost As Decimal = 0.0
    Property import_duty As Decimal = 0.0

End Class

Public Class CBDJson_child_l4ls
    Property l4l_fob As Decimal = 0.0
    Property l4l_extended_fob As Decimal = 0.0
    Property l4l_country As String = ""
End Class

Public Class RefreshResultJSON
    Public Property status As Boolean
    Public Property message As String

End Class

Public Class RefreshTokenResultJSON
    Public Property access_token As String
    Public Property token_type As String
    Public Property expires_in As Integer
    Public Property scope As String
    Public Property id_token As String
    Public Property error_description As String
End Class


