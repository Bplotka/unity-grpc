using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grpc.Core;
using pb = global::Google.Protobuf;

namespace Example
{
    /*
     *  This is class useful to serialize from and to proto.
     */
    public class ProtoMarshaller<T> : Marshaller<T> where T : Google.Protobuf.IMessage<T>
    {
        public ProtoMarshaller(pb::MessageParser<T> parser) :
            base(ProtoMarshaller<T>.Serializer, ProtoMarshaller<T>.DeserializerFn(parser))
        {
        }

        static new byte[] Serializer(T obj)
        {
            byte[] outBytes = new byte[obj.CalculateSize()];
            pb::CodedOutputStream outStream = new pb::CodedOutputStream(outBytes);
            obj.WriteTo(outStream);
            return outBytes;
        }

        static Func<byte[], T> DeserializerFn(pb::MessageParser<T> parser)
        {
            return (serialized) =>
            {
                pb::CodedInputStream inStream = new pb::CodedInputStream(serialized);
                return parser.ParseFrom(inStream);
            };
        }
    }

    /*
     * This is manually wrote Client for Greeter. Normally it would be generated by grpc plugins,
     * but we cannot use it because of break in .Net 3.5 support required by Unity.
     */
    class Client
    {
        private string host;
        private Channel ch;
        private DefaultCallInvoker invoker;


        public Client(string host, int port)
        {
            this.host = host;
            this.ch = new Channel(host, port, ChannelCredentials.Insecure);
            this.invoker = new DefaultCallInvoker(ch);
        }

        /*
         * Missing in Go library, but very useful (not used in this example, though)
         */
        public virtual TResponse BlockingUnaryCall<TRequest, TResponse>(Method<TRequest, TResponse> method, string host, CallOptions options, TRequest request)
            where TRequest : class
            where TResponse : class
        {
            var call = new CallInvocationDetails<TRequest, TResponse>(this.ch, method, host, options);
            return Calls.BlockingUnaryCall(call, request);
        }

        public AsyncServerStreamingCall<Example.HelloReply> SayHello(GrpcCancellationTokenSource ctx, Example.HelloRequest req)
        {
            return this.invoker.AsyncServerStreamingCall<Example.HelloRequest, Example.HelloReply>(
                    new Method<Example.HelloRequest, Example.HelloReply>(
                        MethodType.ServerStreaming,
                        "example.MultiGreeter",
                        "sayHello",
                        new ProtoMarshaller<Example.HelloRequest>(Example.HelloRequest.Parser),
                        new ProtoMarshaller<Example.HelloReply>(Example.HelloReply.Parser)
                    ),
                    this.host,
                    new CallOptions(cancellationToken: new GrpcCancellationToken(ctx)),
                    req
                );
        }
    }


}
