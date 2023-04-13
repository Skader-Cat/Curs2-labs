using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Laba5Console
{
    internal partial class Graph
    {
        public class GraphConverter : JsonConverter<Graph>
        {
            public override void WriteJson(JsonWriter writer, Graph value, JsonSerializer serializer)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("nodes");
                writer.WriteStartArray();
                foreach (var nodes in value.Nodes) {
                    writer.WriteStartObject();
                    writer.WritePropertyName("name");
                    writer.WriteValue(nodes.Name);
                    writer.WritePropertyName("is_final");
                    writer.WriteValue(nodes.IsFinal);
                    writer.WritePropertyName("leave_nodes");

                    writer.WriteStartObject();
                    foreach (var leaveNode in nodes.leave_nodes)
                    {
                        writer.WritePropertyName(leaveNode.Key.Name);
                        writer.WriteStartArray();
                        foreach (var edge_name in leaveNode.Value)
                        {
                            writer.WriteValue(edge_name);
                        }
                        writer.WriteEndArray();
                    }
                    writer.WriteEndObject();
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
                writer.WritePropertyName("alphabet");
                writer.WriteStartArray();
                foreach (var symb in value.Alphabet)
                {
                    writer.WriteValue(symb);
                }
                writer.WriteEndArray();
                writer.WritePropertyName("nodes_cound");
                writer.WriteValue(value.nodesCount);
                writer.WriteEndObject();

            }

            public override Graph ReadJson(JsonReader reader, Type objectType, Graph existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                JObject jObj = JObject.Load(reader);
                Graph graph = new Graph();
                // Deserializing nodes
                JArray jNodes = jObj["nodes"] as JArray;
                if (jNodes != null)
                {
                    foreach (JObject jNode in jNodes)
                    {
                        string nodeName = (string)jNode["name"];
                        bool isFinal = (bool?)jNode["is_final"] ?? false;
                        Node node = new Node(nodeName);
                        node.IsFinal = isFinal;
                        graph.Nodes.Add(node);
                    }
                }

                if (jNodes != null)
                {
                    foreach (JObject jNode in jNodes)
                    {
                        string nodeName = (string)jNode["name"];
                        Node node = graph.Nodes.FirstOrDefault(n => n.Name == nodeName);

                        if (node != null)
                        {
                            JToken jLeaveNodes = jNode["leave_nodes"];
                            if (jLeaveNodes != null)
                            {
                                foreach (JProperty jLeaveNode in jLeaveNodes.Children<JProperty>())
                                {
                                    string leaveNodeName = jLeaveNode.Name;
                                    Node leaveNode = graph.Nodes.FirstOrDefault(n => n.Name == leaveNodeName);

                                    if (leaveNode != null)
                                    {
                                        JArray jEdges = jLeaveNode.Value as JArray;
                                        if (jEdges != null)
                                        {
                                            foreach (JToken jEdge in jEdges)
                                            {
                                                string edgeName = (string)jEdge;
                                                node.leave_nodes.Add(leaveNode, new List<String>{ edgeName });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // Deserializing alphabet and nodes_count
                JArray jAlphabet = jObj["alphabet"] as JArray;
                if (jAlphabet != null)
                {
                    foreach (JToken jToken in jAlphabet)
                    {
                        graph.Alphabet.Add((string)jToken);
                    }
                }

                int nodesCount = (int?)jObj["nodes_count"] ?? 0;
                graph.nodesCount = nodesCount;

                return graph;
            }
        }
    }
}
