		<table>
		<%  
		 foreach (Column col in Table.Columns)
		 {
			$
			<tr class="margin-normal">
			<td>
				<asp:Label runat="server" AssociatedControlID="txt#col.Name " CssClass="label-text">#col.Name :</asp:Label>
				</td><td>
				<asp:TextBox runat="server" ID="txt#col.Name "  CssClass="text-box"/>
				</td>
			</tr>
			$
		 }
		 %>
		 </table>
        <div class="center-text margin-normal">
            <asp:Button runat="server" ID="btnLogin" Text="Submit" OnClick="SubmitEvent" />
        </div>
        <div>
            <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
        </div>