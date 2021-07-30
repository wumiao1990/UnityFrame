
function OnReady()
	-- 控件绑定变量声明，自动生成请勿手改
	--local AreaScrollContent
	--local SvrScrollContent
	-- SubUI
	-- 控件绑定定义结束

	self.CsharpPanel.CurrentUIType.UIForms_Type = CS.SUIFW.UIFormType.PopUp
	
	RigisterButtonObjectEvent(ConfirmBtn, function()
		CloseUIForm()
	end)	
	
	RigisterButtonObjectEvent(RecommendBtn, function()		
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