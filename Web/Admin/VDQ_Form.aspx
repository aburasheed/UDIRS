<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Form.Master" AutoEventWireup="true"
    CodeBehind="VDQ_Form.aspx.cs" Inherits="UDIRS.Web.Admin.VDQ_Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
        // --------------- Max 500 Characters-----------------------------
        function checkMaxLength( e, pThis, spanID ) {
            var nMaxLen = pThis.getAttribute( "TAMaxLength" );
            limitTextarea( pThis, spanID );
            switch ( e.keyCode ) {
                case 37: // left
                    return true;
                case 38: // up
                    return true;
                case 39: // right
                    return true;
                case 40: // down
                    return true;
                case 8: // backspace
                    return true;
                case 46: // delete
                    return true;
                case 27: // escape
                    //pThis.value='';
                    return true;
            }
            if ( pThis.value.length > nMaxLen ) {
                pThis.value = pThis.value.substr( 0, nMaxLen );
            }
            return ( pThis.value.length < nMaxLen );
        }

        function limitTextarea( pThis, spanID ) {
            //var oComments = document.getElementById(pThis.id);
            var oSpanRemaining = document.getElementById( spanID );
            var nMaxLen = pThis.getAttribute( "TAMaxLength" );
            var nCharsRemaining;
            nCharsRemaining = nMaxLen - pThis.value.length;
            if ( nCharsRemaining < 0 ) {
                nCharsRemaining = 0;
            }
            oSpanRemaining.innerHTML = nCharsRemaining + " remaining";
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Main" class="shadowbox" runat="server">
        <div class="divTable">
            <div class="divTable">
                <!-- Row-1 -->
                <div class="divTableBody">
                    <div class="divTableRow">
                        <div style="width: 320px; float: left; text-align: left; padding: 8px 10px; margin: 15px 0px 15px 0px;
                            border: 0.4pt solid #CCCC99; border-style: double;">
                            <b>Refrence Number:</b>
                            <asp:Label ID="lblReference" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                                font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" />
                        </div>
                    </div>
                </div>
                <div class="divTableBody">
                    <div class="divTableRow">
                        <div class="divTableColum">
                            <b>Date/Time:</b></div>
                        <div class="divTableColum" style="width: 280px;">
                            <asp:Label ID="lblDateTime" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                                font-size: 18px; font-style: italic; font-variant: normal;" runat="server" />
                        </div>
                        <div style="width: 100px; float: left; text-align: left; padding: 15px 10px; margin-bottom: 15px;
                            border: 0.4pt solid #999999; border-style: dotted;">
                            <b>Location:</b></div>
                        <div class="divTableColum" style="width: 558px;">
                            <asp:Label ID="lblLocation" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                                font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" /></div>
                    </div>
                </div>
                <!-- Row-2 -->
                <div class="divTableBody">
                    <div class="divTableRow">
                        <div class="divTableColum">
                            <b>Nature of Incident:</b></div>
                        <div class="divTable2ColumData">
                            <asp:Label ID="lblNature" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                                font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" /></div>
                    </div>
                </div>
                <!-- Row-3 -->
                <div class="divTableBody">
                    <div class="divTableRow">
                        <div class="divTableColum">
                            <b>Incident Description:</b></div>
                        <div class="divTable2ColumData">
                            <asp:Label ID="lblIncidentDesc" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                                font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" /></div>
                    </div>
                </div>
                <!-- Row-4 -->
                <div class="divTableBody">
                    <div class="divTableRow">
                        <div class="divTableColum">
                            <b>IsPerson injured:</b><br />
                        </div>
                        <div class="divTable2ColumData">
                            <asp:Label ID="lblPersonInjured" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                                font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" /></div>
                    </div>
                </div>
                <!-- Row-5 -->
                <div class="divTableBody">
                    <div class="divTableRow">
                        <div class="divTableColum">
                            <b>Description of the Injury:</b>
                        </div>
                        <div class="divTable2ColumData">
                            <asp:Label ID="lblInjuryDesc" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                                font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" /></div>
                    </div>
                </div>
                <!-- Row-6 -->
                <div class="divTableBody">
                    <div class="divTableRow">
                        <div class="divTableColum">
                            <b>Treatment Provided:</b></div>
                        <div class="divTableColum" style="width: 280px;">
                            <asp:Label ID="lblTreatmentProvided" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                                font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" /></div>
                        <div style="width: 170px; float: left; text-align: left; padding: 15px 10px; margin-bottom: 15px;
                            border: 0.4pt solid #999999; border-style: dotted;">
                            <b>Type of Treatment:</b></div>
                        <div class="divTableColum" style="width: 488px;">
                            <asp:Label ID="lblTypeTeatment" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                                font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" /></div>
                    </div>
                </div>
                <!-- Row-7 -->
                <div class="divTableBody">
                    <div class="divTableRow">
                        <div class="divTableColum">
                            <b>Suggested Corrective Action:</b></div>
                        <div class="divTable2ColumData">
                            <asp:Label ID="lblSuggestion" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                                font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" /></div>
                    </div>
                </div>
                <!-- Row-8 -->
                <div class="divTableBody">
                    <div class="divTableRow">
                        <div class="divTableColum">
                            <b>Corrective Action:</b></div>
                        <div class="divTable2ColumData">
                            <asp:Label ID="lblAction" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                                font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" />
                        </div>
                    </div>
                </div>
                <!-- Row-9 -->
                <div class="divTableBody">
                    <div class="divTableRow">
                        <div class="divTableColum">
                            <b>Additional Information:</b></div>
                        <div class="divTable2ColumData">
                            <asp:Label ID="lblAddtional" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                                font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" /></div>
                    </div>
                </div>
            </div>
            <!-- RMO -->
            <div class="divTableBody">
                <div class="divTableRow">
                    <div style="width: 1255px; float: left; text-align: Center; padding: 8px 10px; margin: 30px 0px 3px 0px;
                        border: 1pt solid #999999; border-style: double; background-color: #293955; color: white;">
                        <b>Investigation Information<br />
                            Filled by Risk Management Officer</b></div>
                </div>
            </div>
            <div class="divTableBody">
                <div class="divTableRow">
                    <div class="divTableColum" style="margin-bottom: 0px; background-color: #293955;
                        color: white;">
                        <b>Factors contributed to the incident:</b></div>
                    <div class="divTable2ColumRMO">
                        <asp:Label ID="lblFactors" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                            font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" />
                    </div>
                </div>
            </div>
            <div class="divTableBody">
                <div class="divTableRow">
                    <div class="divTableColum dvActions-label vdq-rmo-container">
                        <b>Actions taken to prevent recurrence? </b>
                    </div>
                    <div class="divTable2ColumRMO dvActions-value" style="height: auto;">
                        <%--<asp:Label ID="lblToPrevent" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                            font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" />--%>
                        <asp:GridView ID="gvActionPlan" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None"
                            BorderWidth="1px" CellPadding="4">
                            <Columns>
                                <asp:BoundField DataField="ActionPlanID" HeaderText="ID" ReadOnly="True" Visible="false"
                                    SortExpression="ActionPlanID" />
                                <asp:BoundField HeaderText="ActionTakenToPrevent" DataField="ActionDescription" SortExpression="ActionDescription" />
                                <asp:BoundField HeaderText="Deadline" DataField="Deadline" SortExpression="Deadline" />
                                <asp:BoundField HeaderText="IsCompleted" DataField="IsCompleted" SortExpression="IsCompleted" />
                            </Columns>
                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                            <HeaderStyle BackColor="#2D3C56" Font-Bold="True" ForeColor="#FFFFCC" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <RowStyle BackColor="White" ForeColor="#330099" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                            <SortedAscendingCellStyle BackColor="#FEFCEB" />
                            <SortedAscendingHeaderStyle BackColor="#AF0101" />
                            <SortedDescendingCellStyle BackColor="#F6F0C0" />
                            <SortedDescendingHeaderStyle BackColor="#7E0000" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="divTableBody">
                <div class="divTableRow">
                    <div class="divTableColum" style="margin-bottom: 0px; height: 46px; background-color: #293955;
                        color: white;">
                        <b>Incident Status:</b></div>
                    <div class="divTable2ColumRMO">
                        <asp:Label ID="lblStatus" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                            font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" />
                    </div>
                </div>
            </div>
            <div class="divTableBody">
                <div class="divTableRow">
                    <div class="divTableColum" style="margin-bottom: 0px; height: 115px; background-color: #293955;
                        padding-top: 15px; color: white;">
                        <b>Comments:</b><br />
                        (<span id="spComments">maximum 500 characters</span>)
                    </div>
                    <div style="width: 980px; float: left; text-align: left; padding: 15px 10px; border: 0.4pt solid #999999;
                        border-style: dotted; height: 115px;">
                        <asp:TextBox ID="txtVDQComments" TextMode="MultiLine" TAMaxLength="500" Style="padding-left: 5px;
                            font-family: Georgia, serif; font-size: 18px; font-style: italic; font-variant: normal;
                            margin: 0px; width: 966px; height: 112px;" runat="server" />
                    </div>
                </div>
            </div>
            <div class="divTableBody">
                <div class="divTableRow">
                    <div style="width: 1278px; margin-bottom: 0px; height: 46px; background-color: #293955;
                        padding-top: 20px; color: white;">
                        <asp:Button ID="btnSave" Text="Save" class="control-Btncontainer incident-count-btn"
                            ValidationGroup="Form" runat="server" />  
                        <asp:Button ID="btnSaveSubmit" Text="Save and Send to RMO" class="control-Btncontainer incident-count-btn"
                            ValidationGroup="Form" runat="server" />
                    </div>
                    
                </div>
            </div>
        </div>
        <!-- DivTable.com -->
    </div>
    <script>
        $( document ).ready( function() {
            var height = $( '.dvActions-value' ).css( 'height' );
            $( '.dvActions-label' ).css( 'height', height );

        } );
    </script>
</asp:Content>
