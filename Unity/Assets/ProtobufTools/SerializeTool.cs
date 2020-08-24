using System;
using System.IO;
using Google.Protobuf;


namespace ProtoBuf
{
	public static class SerializeTool
	{
//		/// <summary>  
//		/// 序列化  
//		/// </summary>  
//		/// <typeparam name="T"></typeparam>  
//		/// <param name="msg"></param>  
//		/// <returns></returns>  
//		static public byte[] Serialize<T>(T msg)  
//		{  
//			byte[] result = null;  
//			if (msg != null)  
//			{  
//				using (var stream = new MemoryStream())  
//				{  
//					Serializer.Serialize<T>(stream, msg);  
//					result = stream.ToArray();  
//				}  
//			}  
//			return result;  
//		}  
//
//		/// <summary>  
//		/// 反序列化  
//		/// </summary>  
//		/// <typeparam name="T"></typeparam>  
//		/// <param name="message"></param>  
//		/// <returns></returns>  
//		static public T Deserialize<T>(byte[] message)  
//		{  
//			T result = default(T);  
//			if (message != null)  
//			{  
//				using (var stream = new MemoryStream(message))  
//				{  
//					result = Serializer.Deserialize<T>(stream);  
//				}  
//			}  
//			return result;  
//		}  
		

		/// <summary>
		/// 序列化protobuf 3.0
		/// </summary>
		/// <param name="msg"></param>
		/// <returns></returns>
		public static byte[] Serialize(IMessage msg)
		{
			using (MemoryStream rawOutput = new MemoryStream())
			{
				CodedOutputStream output = new CodedOutputStream(rawOutput);
				//output.WriteRawVarint32((uint)len);
				output.WriteMessage(msg);
				output.Flush();
				byte[] result = rawOutput.ToArray();

				return result;
			}
		}
		/// <summary>
		/// 反序列化protobuf
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dataBytes"></param>
		/// <returns></returns>
		public static T Deserialize<T>(byte[] dataBytes) where T : IMessage, new()
		{
			CodedInputStream stream = new CodedInputStream(dataBytes);
			T msg = new T();
			stream.ReadMessage(msg);
			//msg= (T)msg.Descriptor.Parser.ParseFrom(dataBytes);
			return msg;
		}
    
		
	}
}