--local EventLib = require "Utils/eventlib"
local unpack = unpack or table.unpack

local Event = {}
local events = {}
Event.events = events
function Event.AddListener(event,handler)
	if not event then
		error("event is nil ")
	end
	if not handler or type(handler) ~= "function" then
		error("handler parameter in addlistener function has to be function, " .. type(handler) .. " not right")
	end

	if not events[event] then
		events[event] = {}
	end

	table.insert(events[event], handler)

	return handler
end

function Event.SendEvent(event,...)
	local handlers = events[event]
	if handlers then
		local needDeleteIndex = {}
		for i, handler in ipairs(handlers) do
			local runFinished = handler(...)
			if runFinished then
				table.insert(needDeleteIndex, i)
			end
		end

		if #needDeleteIndex > 0 then
			for i = #needDeleteIndex, 1, -1 do
				local delIndex = needDeleteIndex[i]
				table.remove(handlers, delIndex)
			end
		end
	end
end

function Event.RemoveListener(event,handler)
	local handlers = events[event]
	if handlers then
		local needDeleteIndex = {}
		for i, h in ipairs(handlers) do
			if h == handler then
				table.insert(needDeleteIndex, i)
			end
		end

		if #needDeleteIndex > 0 then
			for i = #needDeleteIndex, 1, -1 do
				local delIndex = needDeleteIndex[i]
				table.remove(handlers, delIndex)
			end
		end
	end
end

function Event.RemoveAllListener(event)
	if events[event] then
		events[event] = {}
	end
end

function Event.Clear()
	events = {}
	Event.events = events
end

return Event