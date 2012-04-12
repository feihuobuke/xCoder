<asp:FormView runat="server" ID="formView">
       <EditItemTemplate>
		<%  
		 foreach (Column col in Table.Columns)
		 {
			$
			<div class="margin-normal">
				<asp:Label runat="server" AssociatedControlID="txt#col.Name " CssClass="label-text">#col.Name :</asp:Label>
				<asp:TextBox runat="server" ID="txt#col.Name " />
			</div>
			$
		 }
		 %>
        
       
        <div class="center-text margin-normal">
            <asp:Button runat="server" ID="btnLogin" Text="Submit" OnClick="SubmitEvent" />
        </div>
        <div>
            <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
        </div>
		</EditItemTemplate>
		<EditItemTemplate>
		<%  
		 foreach (Column col in Table.Columns)
		 {
			$
			<div class="margin-normal">
				<asp:Label runat="server" AssociatedControlID="txt#col.Name " CssClass="label-text">#col.Name :</asp:Label>
				<asp:TextBox runat="server" ID="txt#col.Name " />
			</div>
			$
		 }
		 %>
        
       
        <div class="center-text margin-normal">
            <asp:Button runat="server" ID="btnLogin" Text="Submit" OnClick="SubmitEvent" />
        </div>
        <div>
            <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
        </div>
		</EditItemTemplate>
</asp:FormView>