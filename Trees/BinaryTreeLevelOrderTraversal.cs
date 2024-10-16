using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static Trees.Helper;
using static Trees.Helper.TreeNode;
namespace Trees
{

    internal class BinaryTreeLevelOrderTraversal {
        public void Test() {
            TreeNode test = StrArrayToTree("1,2,3,4,null,null,5");
            var rez = new Solution().LevelOrderOneQueue(test);

            foreach(var vals in rez)
            {
                Console.Write($"[{string.Join(",", vals)}]");
            }
        }

        private class Solution
        {

            public IList<IList<int>> LevelOrderOneQueue(TreeNode root)
            {
                IList<IList<int>> output = new List<IList<int>>();

                if (root == null)
                {
                    return output;
                }

                Queue<TreeNode> queue = new Queue<TreeNode>();


                queue.Enqueue(root);
                int current = 1;
                IList<int> temp;

                while (queue.Count() > 0)
                {
                    int last = 0;
                    temp = null;

                    while (current>0)
                    {
                        var node = queue.Dequeue();
                        (temp ??= new List<int>()).Add(node.val);
                        AddNode(queue, node, ref last);
                        current--;
                    }

                    AddListToLIstIfNotEmpty(output, temp);

                    current = last;
                }

                return output;
            }


            public IList<IList<int>> LevelOrderTwoStacks(TreeNode root)
            {
                IList<IList<int>> output = new List<IList<int>>();

                if (root == null)
                {
                    return output;
                }

                Queue<TreeNode> queue1 = new Queue<TreeNode>();
                Queue<TreeNode> queue2 = new Queue<TreeNode>();

                IList<int> temp;

                queue1.Enqueue(root);
                while (queue1.Count() > 0 || queue2.Count() > 0)
                {
                    temp = null;
                    while (queue1.Count() > 0)
                    {
                        var node = queue1.Dequeue();
                        (temp ??= new List<int>()).Add(node.val);
                        AddNode(queue2, node);
                    }

                    AddListToLIstIfNotEmpty(output, temp);

                    temp = null;

                    while (queue2.Count() > 0)
                    {
                        var node = queue2.Dequeue();
                        (temp ??= new List<int>()).Add(node.val);
                        AddNode(queue1, node);
                    }

                    AddListToLIstIfNotEmpty(output, temp);
                }

                return output;
            }

            public IList<IList<int>> LevelOrderTwoLists(TreeNode root)
            {
                IList<IList<int>> output = new List<IList<int>>();
            
                if(root == null)
                {
                    return output;
                }
            
                IList<TreeNode> list1 = new List<TreeNode>();
                IList<TreeNode> list2 = new List<TreeNode>();

                list1.Add(root);

                while (list1.Count() > 0 || list2.Count() > 0)
                {
                    list2 = new List<TreeNode>();
                    IList<int> outputList1 = new List<int>();

                    foreach (TreeNode node in list1)
                    {
                        outputList1.Add(node.val);

                        AddNode(list2, node);
                    }

                    AddListToLIstIfNotEmpty(output, outputList1);

                    list1 = new List<TreeNode>();
                    IList<int> outputList2 = new List<int>();
                    foreach (TreeNode node in list2)
                    {
                        outputList2.Add(node.val);
                        AddNode(list1, node);
                    }

                    AddListToLIstIfNotEmpty(output, outputList2);
                }


                return output;
            }

            private static void AddListToLIstIfNotEmpty(IList<IList<int>> output, IList<int> toAddList)
            {
                if (toAddList != null && toAddList.Count > 0)
                {
                    output.Add(toAddList);
                }
            }

            private static void AddNode(Queue<TreeNode> queue, TreeNode node, ref int count)
            {

                if (node.left != null)
                {
                    queue.Enqueue(node.left);
                    count++;
                }
                if (node.right != null)
                {
                    queue.Enqueue(node.right);
                    count++;
                }
            }

            private static void AddNode(Queue<TreeNode> queue, TreeNode node)
            {

                if (node.left != null)
                {
                    queue.Enqueue(node.left);
                }
                if (node.right != null)
                {
                    queue.Enqueue(node.right);
                }
            }

            private static void AddNode(IList<TreeNode> list1, TreeNode node)
            {
                if (node.left != null)
                {
                    list1.Add(node.left);
                }

                if (node.right != null)
                {
                    list1.Add(node.right);
                }
            }
        }
    }
}
