function OnReady()	
	
	RigisterButtonObjectEvent(Btn_OK, function()
		CS.SUIFW.UIManager.Instance:ShowUIForms(CS.ProConst.SELECT_HERO_FORM);		
	end)	
	
	RigisterButtonObjectEvent(Btn_Close, function()		
		CloseUIForm()
	end)	
	
	RigisterButtonObjectEvent(Btn_SelectServer, function()
		CS.SUIFW.UIManager.Instance:ShowUIForms("UILoginServerFrom");
	end)
	
	--self.CsharpPanel:SetUpdate(true)
end

function OnHiding()
	print("OnHiding()")
end

function OnUpdate()
	print("OnUpdate()")
end