//-------------------------------------------------------------------------------------
//	DirtyFlagPatternExample.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System;

namespace DirtyFlagPatternExample
{
    public class DirtyFlagPatternExample : MonoBehaviour
    {
        GraphNode graphNode = new GraphNode(new MeshEX());
        TransformEX parentWorldTransform = new TransformEX();
        void Start()
        {
            //Initialize child nodes
            for (int i = 0; i < graphNode.NumChildren; i++)
            {
                graphNode.Children[i] = new GraphNode(new MeshEX());
            }
            //Render
            graphNode.render(TransformEX.origin, true);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //Modify position, trigger dirty mark
                TransformEX newLocalTransform = new TransformEX();
                newLocalTransform.Position = new Vector3(2, 2, 2);
                graphNode.setTransform(newLocalTransform);

                //re-render
                graphNode.render(parentWorldTransform, true);
            }
        }
    }


    class MeshEX
    {

    }


    class TransformEX
    {
        private Vector3 position = new Vector3(1, 1, 1);
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public static TransformEX origin = new TransformEX();

        public TransformEX combine(TransformEX other)
        {

            TransformEX trans = new TransformEX();
            if (other != null)
            {
                trans.Position = Position + other.Position;
            }
            return trans;
        }

    };


    class GraphNode
    {
        //dirty flag
        private bool dirty_;

        private MeshEX mesh_;
        private TransformEX local_;
        private TransformEX world_ = new TransformEX();
        const int MAX_CHILDREN = 100;

        private GraphNode[] children_ = new GraphNode[MAX_CHILDREN];
        public GraphNode[] Children
        {
            get { return children_; }
            set { children_ = value; }
        }

        private int numChildren_ = 88;
        public int NumChildren
        {
            get { return numChildren_; }
            set { numChildren_ = value; }
        }

        public GraphNode(MeshEX mesh)
        {
            mesh_ = mesh;
            local_ = TransformEX.origin;
            dirty_ = true;

        }

        /// <summary>
        /// Set local coordinate position
        /// </summary>
        public void setTransform(TransformEX local)
        {
            local_ = local;
            dirty_ = true;
        }

        public void render(TransformEX parentWorld, bool dirty)
        {

            //If any object above it in the parent chain is marked as dirty, it will be set to true

            dirty |= dirty_;

            //When the node is not changed (dirty=false), skip the combine process, otherwise, it means that the chain is dirty and combine
            if (dirty)
            {
                Debug.Log("this node is dirty,combine it!");
                world_ = local_.combine(parentWorld);
                dirty_ = false;
            }

           
            if (mesh_ != null)
            {
                renderMesh(mesh_, world_);
            }

            for (int i = 0; i < numChildren_; i++)
            {
                if (children_[i] != null)
                {
                    children_[i].render(world_, dirty);
                }

            }
        }

        private void renderMesh(MeshEX mesh_, TransformEX world_)
        {
            Debug.Log("renderMesh!");
        }
    }

}

