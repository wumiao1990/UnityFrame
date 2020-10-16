
function array2table(array)
	local ret = {}
    if array then
        for k=0, array.Length-1 do
            table.insert(ret, array[k])
        end        
    end
	return ret
end

function list2table(list)
	local ret = {}
	if list then
		for k=0, list.Count-1 do
			table.insert(ret, list[k])
		end
	end
	return ret
end

function dic2Table(CSharpDic)
	local dic = {}
	local iter = CSharpDic:GetEnumerator()	
	while iter:MoveNext() do
		dic[iter.Current.Key] = iter.Current.Value
	end
	return dic
end

function string:split(sep)
    local sep, fields = sep or ",", {}
    local pattern = string.format("([^%s]+)", sep)
    self:gsub(pattern, function(c) fields[#fields+1] = c end)
    return fields
end

local function getChildrenNameList(source)	
	local GetChildren = xlua.get_generic_method(CS.UnityEngine.GameObject, 'GetComponentsInChildren')
	local GetChildrenTransform = GetChildren(CS.UnityEngine.Transform)
	local list = GetChildrenTransform(source.gameObject, true)
	local ret = {}
	for i=0,list.Length-1 do
		if not ret[list[i].name] then ret[list[i].name] = {} end
		table.insert(ret[list[i].name], list[i])
	end
	return ret
end

---parseUIVars
---@param source UnityEngine.GameObject
---@param vars table
---@param onlyFullPath boolean
function parseUIVars(source, vars, onlyFullPath)
	local childList = {}
    if not onlyFullPath then
        childList = getChildrenNameList(source.gameObject)
    end
	local ret = {}
	local path, pathVars, name, typeName, obj
	local allFind = true
	for i=1,#vars do
		typeName = vars[i][1]
		for j=2,#vars[i] do
			pathVars = string.split(vars[i][j],"=") 
			path, name = pathVars[1], pathVars[2]
			if name == nil then				
				name = string.split(path, "/") 
				name = name[#name]
			end
			obj = source.transform:Find(path)
			if obj == nil then
				if childList[name] then 
					for k=1,#childList[name] do
						obj = childList[name][k]:GetComponent(typeName)
						if obj then
							ret[name] = obj
							break
						end
					end
				end
			else
				ret[name] = obj:GetComponent(typeName)
			end

			if not ret[name] then 
				print("unable find " .. path)
				allFind = false
			end
		end
	end
	return ret, allFind
end

function table.removeByFunc(tbl, func)
	for i=#tbl,1,-1 do
		if func(tbl[i]) then
			table.remove(tbl, i)
		end
	end
end

function IsNil(uobj) 
	return uobj == nil or (uobj.__tostring and uobj.__tostring() == "<invalid c# object>")  or CS.UtilGame.IsNil(uobj)
end 

function revertTbl(tbl)
    local ret = {}
    for k,v in pairs(tbl) do 
        ret[v] = k
    end
    return ret
end

function getListChange(tb1, tb2)
	tb1 = revertTbl(tb1)
	tb2 = revertTbl(tb2)
	local ret1, ret2 = {}, {}
	for i,j in pairs(tb1) do
		if not tb2[i] then
			table.insert(ret1, i)
		end
	end
	for i,j in pairs(tb2) do
		if not tb1[i] then
			table.insert(ret2, i)
		end
	end
	return ret1, ret2
end

function math.clamp(v, minValue, maxValue)  
    if v < minValue then
        return minValue
    end
    if v > maxValue then
        return maxValue
    end
    return v 
end

function math.rotate(v, minValue, maxValue)
	if v < minValue then
		return maxValue
	end
	if v > maxValue then
		return minValue
	end
	return v
end

function table.clone(org)
    local function copy(org, res)
        for k,v in pairs(org) do
            if type(v) ~= "table" then
                res[k] = v;
            else
                res[k] = {};
                copy(v, res[k])
            end
        end
    end
 
    local res = {}
    copy(org, res)
    return res;
end