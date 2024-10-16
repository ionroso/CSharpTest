using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    public static class Helper
    {
        public class TreeNode
        {
            public int val;
            public TreeNode? left;
            public TreeNode? right;
            public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }

        public static TreeNode StrArrayToTree(string input)
        {

            string[] split = input.Split(",");
            int n = split.Length;
            TreeNode root = new TreeNode(int.Parse(split[0]));
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            int i = 1;
            while (i < split.Length && queue.Count() > 0)
            {
                TreeNode poll = queue.Dequeue();
                if (split[i] != "null")
                {
                    poll.left = new TreeNode(int.Parse(split[i]));
                    queue.Enqueue(poll.left);
                }

                if (i + 1 < n && split[i + 1] != "null")
                {
                    poll.right = new TreeNode(int.Parse(split[i + 1])); ;
                    queue.Enqueue(poll.right);
                }

                i += 2;
            }

            return root;
        }
    }

}
