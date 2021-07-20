function OnReady()	
	
	RigisterButtonObjectEvent(Btn_OK, function()
		CS.SUIFW.UIManager.GetInstance():ShowUIForms(CS.ProConst.SELECT_HERO_FORM);
	end)	
	
	RigisterButtonObjectEvent(Btn_Close, function()		
		CloseUIForm()
	end)
	
	--self.CsharpPanel:SetUpdate(true)
end

function OnHiding()
	print("OnHiding()")
end

function OnUpdate()
	print("OnUpdate()")
end