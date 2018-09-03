using System;
using System.Threading;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TableItems;
using TestStack.White.UIItems.TreeItems;

namespace GMT.White.AutomationFramework.Treev
{
     class  TreeHiearchy 
    {

        public TreeHiearchy()
        {
            
        }

        public static void ExpandTree(Tree managingOfficeRegion)
        {
            foreach (TreeNode parentNode in managingOfficeRegion.Nodes)
            {
                
                TreeNodes childNode = parentNode.Nodes;

                foreach (TreeNode childNode1 in childNode) //Americas
                {
                    childNode1.DoubleClick();
                    TreeNodes childNode2 = childNode1.Nodes;

                    foreach (TreeNode childNode3 in childNode2) // NorthAmericas
                    {
                        childNode3.DoubleClick();
                        TreeNodes childNode4 = childNode3.Nodes;

                        foreach (TreeNode childNode5 in childNode4) //North
                        {
                            childNode5.DoubleClick();
                            TreeNodes childNode6 = childNode5.Nodes;

                            foreach (TreeNode childNode7 in childNode6) //North
                            {
                                childNode7.DoubleClick();
                                TreeNodes childNode8 = childNode7.Nodes;

                                foreach (TreeNode childNode9 in childNode8) //North
                                {
                                    childNode9.DoubleClick();
                                }
                            }
                        }
                    }

                }
            }


        }

    }
}


        

    

        

    





