public class LinkedList
{
    // First and Last element of the LinkedList as well as the Length of it
    private Node First;
    private Node Last;
    public int Length;

    public LinkedList(int begin, int end, int step = 1)
    {
        // Constructs a Node instance with trailing Nodes linked to each other
        this.First = new Node(begin, end, step, null);

        // Gets the Last element
        Node curr = First;
        while (curr.Next != null)
        {
            curr = curr.Next;
        }
        this.Last = curr;

        this.Length = ((end - begin) / step) + 1;
    }

    public void Reverse()
    {
        // Swaps the First and Last and reverse the list
        Node tmp = First;
        this.First = _reverse(First);
        this.Last = tmp;
    }

    private Node _reverse(Node head)
    {
        // Swaps the given Node's Next and Parent Nodes to reverse the list
        Node tmp = head.Next;
        head.Next = head.Parent;
        head.Parent = tmp;
        return head.Parent == null ? head : _reverse(head.Parent);
    }

    public void Push(int value)
    {
        // Pushes an element at the end of the list
        this.Last = this.Last.Next = new Node(value, null, parent: Last);
        this.Length++;
    }

    public void Insert(int index, int value)
    {

        if (index == 0) // Adds to the beginning
        {
            Node tmp = First;
            this.First = new Node(value, next: tmp, parent: null);
            tmp.Parent = First;
            return;
        }

        if (index == Length - 1) // Redirects to Push() for the last element
        {
            Push(value);
            return;
        }

        // Travels to the index and gets the element before it
        Node curr = JumpToIndex(index);

        // Inserts the value at the given index
        curr.Next = new Node(value, next: curr.Next, parent: curr);

        this.Length++;
    }

    public void Extend(int index, LinkedList list)
    {
        if (index == 0) // If at the beginning redirects to ExtendFront()
        {
            ExtendFront(list);
            return;
        }

        // Travels to the index and gets the element before it
        Node curr = JumpToIndex(index);
        // Inserts the given list at the index
        Node tmp = curr.Next;
        curr.Next = list.First;
        list.Last.Next = tmp;

        /*  
        If at the end of the list
        does not change this.Last 
        and sets the Parent field of the new element accordingly 
        */
        if (tmp != null)
        {
            tmp.Parent = list.Last;
        } 
        else 
        {
            this.Last = list.Last;
        }

        this.Length += list.Length;
    }

    public void Extend(LinkedList list)
    {
        // For extending at the end of the list
        int index = Length;
        Extend(index, list);
    }

    public void ExtendFront(LinkedList list)
    {
        Node tmp = First;

        // Changes the First element as the First of the given list
        this.First = list.First;
        // Continues with the old First
        list.Last.Next = tmp;
        // Setting the Parent accordingly
        tmp.Parent = list.Last;

        this.Length += list.Length;
    }

    public int Remove(int index)
    {
        Node deletedNode;
        if (index == 0) // Removes the First element
        {
            deletedNode = First;

            this.First = First.Next; // The new First
            First.Parent = null; // Because First's Parent is always null

            this.Length--;

            return deletedNode.Value;
        }

        if (index == Length - 1) // Redirects to Pop() for the last element
        {
            return Pop();
        }

        // Travels to the element before the given index 
        Node curr = JumpToIndex(index);

        // Gets the to-be-deleted element
        deletedNode = curr.Next;

        // Skips the deleted element and sets the Parent accordingly
        curr.Next = curr.Next.Next;
        curr.Next.Parent = curr;
        // Changes the Last item as it might be different
        this.Last = index + 1 == Length ? curr : Last;

        this.Length--;

        return deletedNode.Value;
    }

    public int Pop()
    {
        Node tmp = this.Last;

        // Removes the last element and sets the Last to the one before
        this.Last = this.Last.Parent;
        this.Last.Next = null;

        this.Length--;

        return tmp.Value;
    }

    public void Sort()
    {
        // Bubble Sort for Warm Up

        for (int i = 0; i < Length - 1; i++)
        {
            // Iterate through the list
            Node curr = First;
            for (int j = 0; j < Length - i - 1; j++)
            {
                // Swap elements if the first one is bigger than the last
                if (curr.Value > curr.Next.Value)
                {
                    // Store the second element
                    Node tmp = curr.Next;

                    // Get it out of the line
                    curr.Next = tmp.Next;
                    if (tmp.Next != null)
                    {
                        tmp.Next.Parent = curr;
                    }

                    // Put it back before the first element
                    tmp.Next = curr;
                    tmp.Parent = curr.Parent;

                    if (curr.Parent.Next != null)
                    {
                        curr.Parent.Next = tmp;
                    }

                    curr.Parent = tmp;

                    // In case the First element changed
                    if (j == 0)
                    {
                        this.First = tmp;
                    }

                    // In case the Last element changed
                    if (j == Length - 1)
                    {
                        this.Last = tmp;
                    }

                }
                else {
                    // Next element
                    curr = curr.Next;
                }
            }
        }

    }

    public int[] WriteToArray()
    {
        // Init the return array
        int[] array = new int[Length];

        // Iterate over the list
        Node curr = First;
        for (int i = 0; i < Length; i++)
        {
            // Write the values to the array
            array[i] = curr.Value;
            curr = curr.Next;
        }

        return array;
    }

    private Node JumpToIndex(int index)
    {
        // Travels to the element before the given index 
        // to skip the to-be-deleted element
        // Optimized for indices after the half of the list
        Node node;
        if (index > Length / 2)
        {
            node = Last;
            for (int i = Length - 1; i >= index; i--)
            {
                node = node.Parent;
            }
        }
        else {
            node = First;
            for (int i = 0; i < index - 1; i++)
            {
                node = node.Next;
            }
        }
        return node;
    }

    public override string ToString()
    {
        string sb = "";
        Node curr = First;
        while (curr != null)
        {
            sb += string.Format("{0} -> ", curr.Value);
            curr = curr.Next;
        }
        sb += "null";
        return sb;
    }


    private class Node
    {
        public int Value;
        public Node Next;
        public Node Parent;

        public Node(int value, Node next, Node parent)
        {
            this.Value = value;
            this.Next = next;
            this.Parent = parent;
        }

        public Node(int begin, int end, int step, Node parent)
        {
            this.Value = begin;
            this.Parent = parent;
            this.Next = begin + step > end ? null : new Node(begin + step, end, step, this);
        }

    }
}
