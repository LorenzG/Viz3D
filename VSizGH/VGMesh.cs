using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using FirstToolWin.Utilities;
using FirstToolWin.Kernel;
using FirstToolWin.Geometry.Mesh;

namespace VSizGH
{
    public class VGMesh : MetaObjectMesh<VGPoint3d>
    {
        public const string type_Mesh = "Rhino.Geometry.Mesh";

        public VGMesh()
            : base()
        {
        }
        public override bool IsValid()
        {
            return expression.Type == type_Mesh;
        }
        public override string DebuggerTypeName
        {
            get { return type_Mesh; }
        }
        public override IEnumerable<IEnumerable<int>> Face_Names
        {
            get
            {
                Logger.Log(this, "Name is: " + expression.Name);
                Logger.Log(this, "Value is: " + expression.Value);
                Logger.Log(this, "Looking for # of faces");


                Expression expFaces = FindChildren("Faces");
                Expression expCount = FindChildren(expFaces, "Count");


                int count = int.Parse(expCount.Value);

                Logger.Log(this, "Found " + expFaces.Value + " faces");

                //List<int[]> conn = new List<int[]>();


                for (int i = 0; i < count; i++)
                {
                    string pName = "Faces[" + i + "]";
                    Logger.Log(this, "Face " + pName);


                    Logger.Log(this, "VGMesh - iter:" + expression.Name + "." + pName);
                    
                    Expression expFace = expression.DTE.Debugger.GetExpression(expression.Name + "." + pName);


                    if (expFace == null)
                    {
                        Logger.Log(this, "VGMesh - Children is null");
                        continue;
                    }
                    Logger.Log(this, "VGMesh - Got Children: " + expFace.Name);

                    if (!expFace.IsValidValue)
                    {
                        Logger.Log(this, "VGMesh - Children is invalid");
                        continue;
                    }

                    VGMeshFace face = VGMeshFace.CreateAndRun<VGMeshFace>(expFace);

                    Logger.Log(this, "VGMesh - p is null: " + (face == null));
                    Logger.Log(this, "VGMesh - GotExpression");

                    if (face != null && face.IsValid())
                    {
                        Logger.Log(this, "VGMesh - Got face " + pName);


                        int[] idxs;

                        if (!face.isQuad)
                        {
                            idxs = new int[3];

                            idxs[0] = face.a;
                            idxs[1] = face.b;
                            idxs[2] = face.c;
                        }
                        else
                        {
                            idxs = new int[4];

                            idxs[0] = face.a;
                            idxs[1] = face.b;
                            idxs[2] = face.c;
                            idxs[3] = face.d;
                        }

                        //conn.Add(idxs);

                        Logger.Log(this, "VGMesh - face added: " + pName);
                        yield return idxs;
                    }
                }





                ///////////



                //Expression expFaces = FindChildren("Faces");
                //for (int i = 0; i < count; i++)
                //{
                //    string ret = "[" + i + "]";

                //    Logger.Log(this, "Getting face " + i);
                //    Expression expFace = FindChildrenNODOT(expFaces, ret);

                //    string ret1 = ".GetFace(" + i + ")";

                //    Logger.Log(this, "Getting face " + i);
                //    Expression expFace1 = FindChildrenNODOT(expFaces, ret1);
                //    Logger.Log(this, "Got face " + i + ": " + expFace.IsValidValue);



                //    if (expFace.IsValidValue)
                //    {
                //        Logger.Log(this, "expFace is valid ");
                //        expFace = expFace;
                //    }
                //    else if (expFace1.IsValidValue)
                //    {
                //        Logger.Log(this, "expFace1 is valid ");
                //        expFace = expFace1;
                //    }
                //    else
                //    {
                //        Logger.Log(this, "Getting face " + i);
                //        foreach (EnvDTE.Expression e in expFaces.DataMembers)
                //        {
                //            Logger.Log(this, "Got face " + i + ": " + e.IsValidValue);
                //        }
                //    }

                //    List<int> l = new List<int>(4);


                //    Logger.Log(this, "yielding " + ret);
                //    yield return l;
                //}

            }
        }
        public override IEnumerable<string> Node_Names
        {
            get
            {
                Logger.Log(this, "NODE NAME");

                Logger.Log(this, "Name is: " + expression.Name);
                Logger.Log(this, "Value is: " + expression.Value);
                Logger.Log(this, "Looking for # of vertices");


                Expression expVertices = FindChildren("Vertices");
                Expression expCount = FindChildren(expVertices, "Count");


                int count = int.Parse(expCount.Value);

                Logger.Log(this, "Found " + expVertices.Value + " vertices");


                for (int i = 0; i < count; i++)
                {
                    string pName = "[" + i + "]";
                    Logger.Log(this, "Vertex " + pName);


                    Logger.Log(this, "VGMesh - iter:" + pName);

                    Expression expVertex = expVertices.DTE.Debugger.GetExpression(pName);

                    if (expVertex == null)
                    {
                        Logger.Log(this, "VGMesh - Children is null");
                        continue;
                    }
                    Logger.Log(this, "VGMesh - Got Children: " + expVertex.Name);

                    if (!expVertex.IsValidValue)
                    {
                        Logger.Log(this, "VGMesh - Children is invalid");
                        continue;
                    }

                }

                string s = "Vertices";

                for (int i = 0; i < count; i++)
                {
                    string ret = s + "[" + i + "]";
                    Logger.Log(this, "yielding " + ret);
                    yield return ret;
                }
            }
        }
        public override IEnumerable<VGPoint3d> Nodes
        {
            get
            {
                Logger.Log(this, "NODES");

                Logger.Log(this, "Name is: " + expression.Name);
                Logger.Log(this, "Value is: " + expression.Value);
                Logger.Log(this, "Looking for # of vertices");


                Expression expVertices = FindChildren("Vertices");
                Expression expCount = FindChildren(expVertices, "Count");


                //string varName = "_" + Guid.NewGuid().ToString("N");
                //expression.DTE.Debugger.ExecuteStatement(string.Format("int {0} = {1}.{2};", varName, expression.Name, expCount.Name));

                //EnvDTE.Expression tempVar = expression.DTE.Debugger.GetExpression(varName);

                //if (!tempVar.IsValidValue)
                //{

                //}

                //expression.DTE.Debugger.ExecuteStatement(string.Format("{0} = null;", varName));



                int count = int.Parse(expCount.Value);

                Logger.Log(this, "Found " + count + " vertices");
                

                for (int i = 0; i < count; i++)
                {
                    string pName = "Vertices[" + i + "]";
                    Logger.Log(this, "Vertex" + pName);


                    Logger.Log(this, "VGMesh - iter:" + pName);

                    Expression expVertex = expression.DTE.Debugger.GetExpression(expression.Name + "." + pName);

                    if (!expVertex.IsValidValue)
                    {
                        Logger.Log(this, "VGMesh - Children is invalid 1");
                        continue;
                    }

                    //expVertex = expression.DataMembers.DTE.Debugger.GetExpression(pName);

                    //if (!expVertex.IsValidValue)
                    //{
                    //    Logger.Log(this, "VGMesh - Children is invalid 2");
                    //    continue;
                    //}


                    //expVertex = expression.Parent.DTE.Debugger.GetExpression(pName);

                    //if (!expVertex.IsValidValue)
                    //{
                    //    Logger.Log(this, "VGMesh - Children is invalid 3");
                    //    continue;
                    //}

                    Logger.Log(this, "VGMesh - Got Children: " + expVertex.Name);

                    if (!expVertex.IsValidValue)
                    {
                        Logger.Log(this, "VGMesh - Children is invalid");
                        continue;
                    }

                    VGPoint3d pnt = VGPoint3d.CreateAndRun<VGPoint3d>(expVertex);

                    yield return pnt;
                }

            }
        }

    }
}
