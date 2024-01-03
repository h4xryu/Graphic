using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Graphic
{
    public class ObjMesh
    {
        public bool is_loaded = false;
        public ObjMesh(string fileName)
        {
            is_loaded = ObjMeshLoader.Load(this, fileName);
        }

        public ObjVertex[] Vertices
        {
            get { return vertices; }
            set { vertices = value; }
        }
        ObjVertex[] vertices;

        public ObjTriangle[] Triangles
        {
            get { return triangles; }
            set { triangles = value; }
        }
        ObjTriangle[] triangles;

        public ObjQuad[] Quads
        {
            get { return quads; }
            set { quads = value; }
        }
        ObjQuad[] quads;

        int verticesBufferId = 0;
        int trianglesBufferId = 0;
        int quadsBufferId = 0;

        public void Prepare()
        {
            if (!is_loaded) return;
            if (verticesBufferId == 0)
            {
                GL.GenBuffers(1, out verticesBufferId);
                GL.BindBuffer(BufferTarget.ArrayBuffer, verticesBufferId);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * Marshal.SizeOf(typeof(ObjVertex))), vertices, BufferUsageHint.StaticDraw);
            }

            if (trianglesBufferId == 0)
            {
                GL.GenBuffers(1, out trianglesBufferId);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, trianglesBufferId);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(triangles.Length * Marshal.SizeOf(typeof(ObjTriangle))), triangles, BufferUsageHint.StaticDraw);
            }

            if (quadsBufferId == 0)
            {
                GL.GenBuffers(1, out quadsBufferId);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, quadsBufferId);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(quads.Length * Marshal.SizeOf(typeof(ObjQuad))), quads, BufferUsageHint.StaticDraw);
            }
        }

        public void Render()
        {
            if (!is_loaded) return;
            GL.PushClientAttrib(ClientAttribMask.ClientVertexArrayBit);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, verticesBufferId);
            GL.InterleavedArrays(InterleavedArrayFormat.T2fN3fV3f, Marshal.SizeOf(typeof(ObjVertex)), IntPtr.Zero);
            if (triangles.Length > 0)
            {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, trianglesBufferId);
                GL.DrawElements(BeginMode.Triangles, triangles.Length * 3, DrawElementsType.UnsignedInt, 0);
            }

            if (quads.Length > 0)
            {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, quadsBufferId);
                GL.DrawElements(BeginMode.Quads, quads.Length * 4, DrawElementsType.UnsignedInt, 0);
            }

            GL.PopClientAttrib();
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct ObjVertex
        {
            public Vector2 TexCoord;
            public Vector3 Normal;
            public Vector3 Vertex;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ObjTriangle
        {
            public UInt32 Index0;
            public UInt32 Index1;
            public UInt32 Index2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ObjQuad
        {
            public UInt32 Index0;
            public UInt32 Index1;
            public UInt32 Index2;
            public UInt32 Index3;
        }
    }
    public class ObjMeshLoader
    {
        public static bool Load(ObjMesh mesh, string fileName)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(fileName))
                {
                    Load(mesh, streamReader);
                    streamReader.Close();
                    return true;
                }
            }
            catch { return false; }
        }

        static char[] splitCharacters = new char[] { ' ' };

        static List<Vector3> vertices;
        static List<Vector3> normals;
        static List<Vector2> texCoords;
        static Dictionary<ObjMesh.ObjVertex, UInt32> objVerticesIndexDictionary;
        static List<ObjMesh.ObjVertex> objVertices;
        static List<ObjMesh.ObjTriangle> objTriangles;
        static List<ObjMesh.ObjQuad> objQuads;

        static void Load(ObjMesh mesh, TextReader textReader)
        {
            vertices = new List<Vector3>();
            normals = new List<Vector3>();
            texCoords = new List<Vector2>();
            objVerticesIndexDictionary = new Dictionary<ObjMesh.ObjVertex, UInt32>();
            objVertices = new List<ObjMesh.ObjVertex>();
            objTriangles = new List<ObjMesh.ObjTriangle>();
            objQuads = new List<ObjMesh.ObjQuad>();

            string line;
            while ((line = textReader.ReadLine()) != null)
            {
                line = line.Trim(splitCharacters);
                line = line.Replace("  ", " ");

                string[] parameters = line.Split(splitCharacters);

                switch (parameters[0])
                {
                    case "p": // Point
                        break;

                    case "v": // Vertex
                        float x = float.Parse(parameters[1]);
                        float y = float.Parse(parameters[2]);
                        float z = float.Parse(parameters[3]);
                        vertices.Add(new Vector3(x, y, z));
                        break;

                    case "vt": // TexCoord
                        float u = float.Parse(parameters[1]);
                        float v = float.Parse(parameters[2]);
                        texCoords.Add(new Vector2(u, v));
                        break;

                    case "vn": // Normal
                        float nx = float.Parse(parameters[1]);
                        float ny = float.Parse(parameters[2]);
                        float nz = float.Parse(parameters[3]);
                        normals.Add(new Vector3(nx, ny, nz));
                        break;

                    case "f":
                        switch (parameters.Length)
                        {
                            case 4:
                                ObjMesh.ObjTriangle objTriangle = new ObjMesh.ObjTriangle();
                                objTriangle.Index0 = ParseFaceParameter(parameters[1]);
                                objTriangle.Index1 = ParseFaceParameter(parameters[2]);
                                objTriangle.Index2 = ParseFaceParameter(parameters[3]);
                                objTriangles.Add(objTriangle);
                                break;

                            case 5:
                                ObjMesh.ObjQuad objQuad = new ObjMesh.ObjQuad();
                                objQuad.Index0 = ParseFaceParameter(parameters[1]);
                                objQuad.Index1 = ParseFaceParameter(parameters[2]);
                                objQuad.Index2 = ParseFaceParameter(parameters[3]);
                                objQuad.Index3 = ParseFaceParameter(parameters[4]);
                                objQuads.Add(objQuad);
                                break;
                        }
                        break;
                }
            }

            mesh.Vertices = objVertices.ToArray();
            mesh.Triangles = objTriangles.ToArray();
            mesh.Quads = objQuads.ToArray();

            objVerticesIndexDictionary = null;
            vertices = null;
            normals = null;
            texCoords = null;
            objVertices = null;
            objTriangles = null;
            objQuads = null;
        }

        static char[] faceParamaterSplitter = new char[] { '/' };
        static UInt32 ParseFaceParameter(string faceParameter)
        {
            Vector3 vertex = new Vector3();
            Vector2 texCoord = new Vector2(0, 0);
            Vector3 normal = new Vector3();

            string[] parameters = faceParameter.Split(faceParamaterSplitter, StringSplitOptions.None);

            int vertexIndex = int.Parse(parameters[0]);
            if (vertexIndex < 0) vertexIndex = vertices.Count + vertexIndex;
            else vertexIndex = vertexIndex - 1;
            vertex = vertices[vertexIndex];

            if (parameters.Length > 1 && texCoords.Count > 0 && parameters[1].Length > 0)
            {
                int texCoordIndex = int.Parse(parameters[1]);
                if (texCoordIndex < 0) texCoordIndex = texCoords.Count + texCoordIndex;
                else texCoordIndex = texCoordIndex - 1;
                texCoord = texCoords[texCoordIndex];
            }

            if (parameters.Length > 2 && normals.Count > 0)
            {
                int normalIndex = int.Parse(parameters[2]);
                if (normalIndex < 0) normalIndex = normals.Count + normalIndex;
                else normalIndex = normalIndex - 1;
                normal = normals[normalIndex];
            }

            return FindOrAddObjVertex(ref vertex, ref texCoord, ref normal);
        }

        static UInt32 FindOrAddObjVertex(ref Vector3 vertex, ref Vector2 texCoord, ref Vector3 normal)
        {
            ObjMesh.ObjVertex newObjVertex = new ObjMesh.ObjVertex();
            newObjVertex.Vertex = vertex;
            newObjVertex.TexCoord = texCoord;
            newObjVertex.Normal = normal;

            UInt32 index;
            if (objVerticesIndexDictionary.TryGetValue(newObjVertex, out index))
            {
                return index;
            }
            else
            {
                objVertices.Add(newObjVertex);
                objVerticesIndexDictionary[newObjVertex] = (UInt32)(objVertices.Count - 1);
                return (UInt32)(objVertices.Count - 1);
            }
        }
    }
}
