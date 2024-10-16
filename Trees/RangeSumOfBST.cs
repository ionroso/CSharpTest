using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Trees.Helper;

namespace Trees
{
    public class RangeSumOfBST
    {
        public class Sum
        {
            private int _sum = 0;

            public void SumUp(int num) => _sum += num;

            public int GetTotal() => _sum;

        }

        public void Test()
        {
            TreeNode test = StrArrayToTree("10,5,15,3,7,null,18");
            Console.WriteLine(new Solution().RangeSumBST(test, 7, 15));
        }

        private class Solution
        {
            public int RangeSumBST(TreeNode root, int low, int high)
            {
                Sum sum = new();
                InorderItr(root, low, high, sum);

                return sum.GetTotal();
            }

            private void InOrderRec(TreeNode root, int low, int high, Sum sum)
            {
                if(root == null) return;

                if (root.val > low) {
                    InOrderRec(root.left, low, high, sum);
                }

                if (root.val < high)
                {
                    InOrderRec(root.right, low, high, sum);
                }

            }

            private void InorderItr(TreeNode root, int low, int high, Sum sum)
            {
                if (root == null) return;

                Stack<TreeNode> s = new();
                TreeNode curr = root;

                // Traverse the tree
                while (curr != null || s.Count > 0)
                {
                    // Reach the left most Node of the curr Node
                    while (curr != null && (curr.val > low))
                    {
                        // Place pointer to a tree node on the stack
                        // before traversing the node's left subtree
                        s.Push(curr);
                        curr = curr.left;
                    }

                    curr = curr??s.Pop();
                    
                    if (curr.val >= low && curr.val <= high)
                    {
                        Console.WriteLine(curr.val);
                        sum.SumUp(curr.val);
                    }


                    // we have visited the node and its left subtree.
                    // Now, it's right subtree's turn
                    curr = curr.right;
                }
            }
        }
    }
}
