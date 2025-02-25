using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pathfinder
{
    public class Node
    {

        public Vector3 position;
        
        public float G { get; private set; }
        public float H { get; private set; }
        public float F {
            get {
                return G + H;
            }
        }

        public float moveCost = 1;

        //public bool isSolid;

        public List<Node> neighbors = new List<Node>();

        public Node parent { get; private set; }
        
        public void UpdateParentAndG(Node parent, float extraG = 0)
        {
            this.parent = parent;
            if(parent != null) {
                G = parent.G + moveCost + extraG;
            } else {
                G = extraG;
            }
        }

        // Makes an educated guess as to how far we are from end
        public void DoHeuristic(Node end)
        {
            // euclidean heuristic:
            Vector3 d = end.position - this.position;
            H = d.magnitude;

            // manhattan heuristic:
            // H = d.x + d.y + d.z;
        }

    }
    public static List<Node> Solve(Node start, Node end)
    {
        if (start == null || end == null) return new List<Node>();        
        
        List<Node> open = new List<Node>(); // all the nodes that have been discovered, but not 'scanned'
        List<Node> closed = new List<Node>(); // these nodes are 'scanned'

        start.UpdateParentAndG( null );
        open.Add(start);

        // 1. travel from start to end
        while(open.Count > 0) {
            // find node in OPEN list with SMALLEST F value
            float bestF = 0;
            Node current = null;
            foreach(Node n in open) {
                if(n.F < bestF || current == null) {
                    current = n;
                    bestF = n.F;
                }
            }
            // if this node is the end, stop looping
            if(current == end) {
                break;
            }
            bool isDone = false;

            foreach (Node neighbor in current.neighbors)
            {
                if (!closed.Contains(neighbor)) { // node not in CLOSED
                    if (!open.Contains(neighbor)) { // node not in OPEN
                        
                        open.Add(neighbor);

                        float dis = (neighbor.position - current.position).magnitude;

                        neighbor.UpdateParentAndG(current, dis); // set child's 'parent' & 'G'

                        if(neighbor == end) {
                            isDone = true;
                        }
                        neighbor.DoHeuristic(end);

                    }
                    else
                    { // node already in OPEN

                        // if this path to neighbor has lower G
                        // than previous path to neighbor
                        float dis = (neighbor.position - current.position).magnitude;
                        if(current.G + neighbor.moveCost + dis < neighbor.G)
                        {
                            neighbor.UpdateParentAndG(current, dis);
                        }
                    }
                }
            }

            closed.Add(current);
            open.Remove(current);

            if (isDone) break;
        }

        // 2. travel from end to start, building path
        List<Node> path = new List<Node>();
        /*
        Node temp = end;
        while(temp != null)
        {
            path.Add(temp);
            temp = temp.parent;
        }
        */
        for(Node temp = end; temp != null; temp = temp.parent)
        {
            path.Add(temp);
        }


        // 3. reverse path
        path.Reverse();
        
        return path;

    }


}

