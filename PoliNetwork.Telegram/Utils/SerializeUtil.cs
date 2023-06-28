using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace SampleNuGet.Utils;

//System.Runtime.Serialization.Formatters.Binary.BinaryFormatter is obsolete, replaced with
//https://stackoverflow.com/a/53078979

/// <summary>
///     Serialization util
/// </summary>
[PublicAPI]
public static class SerializeUtil
{
    /// <summary>
    ///     Serialize object
    /// </summary>
    /// <param name="value">object to serialize</param>
    /// <returns>byte array of serialized object</returns>
    public static byte[] SerializeObject(object? value)
    {
        return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value));
    }

    /// <summary>
    ///     Deserialize object, given the type (T)
    /// </summary>
    /// <param name="bytes">byte array of the serialized object to deserialize</param>
    /// <typeparam name="T">type of the object</typeparam>
    /// <returns>the deserialized object</returns>
    public static T? DeserializeObject<T>(byte[] bytes)
    {
        var x = Encoding.UTF8.GetString(bytes);
        return JsonConvert.DeserializeObject<T>(x);
    }

    /// <summary>
    ///     Get memory stream from byte array
    /// </summary>
    /// <param name="bytes">byte array</param>
    /// <returns>memory stream</returns>
    public static MemoryStream GetMemoryStreamFromByteArray(byte[] bytes)
    {
        return new MemoryStream(bytes);
    }


    public static string JsonToString(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    [Obsolete("Obsolete")]
    public static void SerializeObjectToStream<T>(T objectToWrite, ref Stream? stream)
    {
        var binaryFormatter = new BinaryFormatter();
        if (stream == null) return;
        if (objectToWrite != null)
            binaryFormatter.Serialize(stream, objectToWrite);
    }

    /// <summary>
    ///     Reads an object instance from a binary file.
    /// </summary>
    /// <typeparam name="T">The type of object to read from the binary file.</typeparam>
    /// <param name="filePath">The file path to read the object instance from.</param>
    /// <returns>Returns a new instance of the object read from the binary file.</returns>
    [Obsolete("Obsolete")]
    public static T? ReadFromBinaryFile<T>(string filePath)
    {
        Stream? stream = null;
        try
        {
            stream = File.Open(filePath, FileMode.Open);
            var binaryFormatter = new BinaryFormatter();
            try
            {
                var r = (T)binaryFormatter.Deserialize(stream);
                try
                {
                    stream.Close();
                }
                catch
                {
                    // ignored
                }

                return r;
            }
            catch
            {
                try
                {
                    stream.Close();
                }
                catch
                {
                    // ignored
                }

                return default;
            }
        }
        catch
        {
            try
            {
                stream?.Close();
            }
            catch
            {
                // ignored
            }

            return default;
        }
    }
}