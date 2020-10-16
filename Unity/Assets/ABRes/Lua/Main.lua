require("commonFunc")
require("Events")

local function update()	
	if #updateFuncList == 0 then return end
	local currentTime = CS.UnityEngine.Time.unscaledTime
	for i = 1, #updateFuncList do
		if updateFuncList[i] and not updateFuncList[i].isFinish and currentTime >= updateFuncList[i].startTime then
			if updateFuncList[i].isDelayFunc then
				updateFuncList[i].func()
				updateFuncList[i].isFinish = true
			else
				if updateFuncList[i].endTime == - 1 or currentTime <= updateFuncList[i].endTime then
					if not updateFuncList[i].isFinish then
						updateFuncList[i].startTime = updateFuncList[i].startTime + updateFuncList[i].interval
						updateFuncList[i].func()
					end
				else
					updateFuncList[i].isFinish = true
				end
			end
		end
	end
	
	for i = #updateFuncList, 1, - 1 do
		if updateFuncList[i].isFinish then
			table.remove(updateFuncList, i)
		end
	end
end

function addUpdateFunc(func, interval, delay, endTime, tag)
	local ret = {func = func, interval = interval or 0, startTime =(delay or 0) + CS.UnityEngine.Time.unscaledTime, endTime = time and time + CS.UnityEngine.Time.unscaledTime or - 1, tag = tag}
	table.insert(updateFuncList, ret)
	return ret
end

function removeUpdateFunc(func)
	for i = 1, #updateFuncList do
		if updateFuncList[i] == func then
			updateFuncList[i].isFinish = true
		end
	end
end

function addDelayFunc(func, delay, tag)
	local ret = {func = func, startTime =(delay or 0) + CS.UnityEngine.Time.unscaledTime, isDelayFunc = true, tag = tag}
	table.insert(updateFuncList, ret)
	return ret
end

function removeFuncByTag(tag)
	for i = #updateFuncList, 1, - 1 do
		if updateFuncList[i].tag and updateFuncList[i].tag == tag then
			updateFuncList[i].isFinish = true
		end
	end
end

local function init()
	CS.LuaEnvManager.Instanse.updateLua = update
	updateFuncList = {}
end

function clearUpdateList()
	updateFuncList = {}
end

init()
print("lua main start")

