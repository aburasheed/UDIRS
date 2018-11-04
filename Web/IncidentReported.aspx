<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Form.Master" AutoEventWireup="true" CodeBehind="IncidentReported.aspx.cs" Inherits="UDIRS.Web.IncidentReported" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Main" class="shadowbox" runat="server">
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
                            font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="180117_10:10" />
                    </div>
                    <div style="width: 100px; float: left; text-align: left; padding: 15px 10px; margin-bottom:15px; border: 0.4pt solid #999999; border-style: dotted;">
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
                    <div style="width: 170px; float: left; text-align: left; padding: 15px 10px; margin-bottom:15px; border: 0.4pt solid #999999; border-style: dotted;">
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

    <!-- RMO -->
            <div class="divTableBody">
                <div class="divTableRow">
                    <div style="width: 1255px; float: left; text-align: Center; padding: 8px 10px; margin: 30px 0px 3px 0px;
                        border: 1pt solid #999999; border-style: double; background-color: #293955; color:white;">
                        <b>Investigation Information<br /> Filled by Risk Management Officer</b></div>
                </div>
            </div>

            <div class="divTableBody">
                <div class="divTableRow">
                    <div class="divTableColum" style="margin-bottom:0px; background-color: #293955; color:white;">
                        <b>Factors contributed to the incident:</b></div>
                    <div class="divTable2ColumRMO">
                        <asp:Label ID="lblFactors" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                            font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" />
                    </div>
                </div>
            </div>

            <div class="divTableBody">
                <div class="divTableRow">
                    <div class="divTableColum" style="margin-bottom:0px; background-color: #293955; color:white;">
                        <b>Actions taken to prevent recurrence? </b></div>
                    <div class="divTable2ColumRMO">
                        <asp:Label ID="lblToPrevent" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                            font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" />
                    </div>
                </div>
            </div>

            <div class="divTableBody">
                <div class="divTableRow">
                    <div class="divTableColum" style="margin-bottom:0px; height:46px; background-color: #293955; color:white;">
                        <b>Status:</b></div>
                    <div class="divTable2ColumRMO">
                        <asp:Label ID="lblStatus" Style="padding-left: 5px; font-family: Lucida Bright, Georgia, serif;
                            font-size: 18px; font-style: italic; font-variant: normal;" runat="server" Text="" />
                    </div>
                </div>
            </div>

<%--
            <div class="divTableBody">
                <div class="divTableRow">
                    <div >
                        Location:</div>
                    <div >
                        &nbsp;</div>
                    <div >
                        Nature of Incidents:</div>
                    <div >
                        &nbsp;</div>
                </div>
            </div>

            <div class="divTableBody">
                <div class="divTableRow">
                    <div >
                        Incident Description:</div>
                    <div >
                        &nbsp;</div>
                </div>
            </div>

            <div class="divTableBody">
                <div class="divTableRow">
                    <div >
                        Injury:</div>
                    <div >
                        &nbsp;</div>
                    <div >
                        Description of the injury:</div>
                    <div >
                        &nbsp;</div>
                </div>
            </div>

            <div class="divTableBody">
                <div class="divTableRow">
                    <div >
                        Medical treatment provided:</div>
                    <div >
                        &nbsp;</div>
                    <div >
                        Type of Medical treatment:</div>
                    <div >
                        &nbsp;</div>
                </div>
            </div>

            <div class="divTableBody">
                <div class="divTableRow">
                    <div >
                        Corrective actions:</div>
                    <div >
                        &nbsp;</div>
                </div>
            </div>

            <div class="divTableBody">
                <div class="divTableRow">
                    <div >
                        Additional Information:</div>
                    <div >
                    </div>
                </div>
            </div>--%>

        </div>
        <!-- DivTable.com -->
    </div>
</asp:Content>
