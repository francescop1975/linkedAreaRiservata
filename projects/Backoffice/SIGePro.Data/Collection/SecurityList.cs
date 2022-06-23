using System;
using System.Collections;
using Init.SIGePro.Authentication;

namespace Init.SIGePro.Collection {
    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='SecurityInfo'/> objects.
    ///    </para>
    /// </summary>
    /// <seealso cref='SecurityList'/>
    [Serializable()]
    public class SecurityList : CollectionBase {
        
        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='SecurityList'/>.
        ///    </para>
        /// </summary>
        public SecurityList() {
        }
        
        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='SecurityList'/> based on another <see cref='SecurityList'/>.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A <see cref='SecurityList'/> from which the contents are copied
        /// </param>
        public SecurityList(SecurityList value) {
            this.AddRange(value);
        }
        
        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='SecurityList'/> containing any array of <see cref='SecurityInfo'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='SecurityInfo'/> objects with which to intialize the collection
        /// </param>
        public SecurityList(SecurityInfo[] value) {
            this.AddRange(value);
        }
        
        /// <summary>
        /// <para>Represents the entry at the specified index of the <see cref='SecurityInfo'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> is outside the valid range of indexes for the collection.</exception>
		public SecurityInfo this[int index] {
            get {
                return ((SecurityInfo)(List[index]));
            }
            set {
                List[index] = value;
            }
        }
        
        /// <summary>
        ///    <para>Adds a <see cref='SecurityInfo'/> with the specified value to the 
        ///    <see cref='SecurityList'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='SecurityInfo'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='SecurityList.AddRange'/>
        public int Add(SecurityInfo value) {
            return List.Add(value);
        }
        
        /// <summary>
        /// <para>Copies the elements of an array to the end of the <see cref='SecurityList'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='SecurityInfo'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='SecurityList.Add'/>
        public void AddRange(SecurityInfo[] value) {
            for (int i = 0; (i < value.Length); i = (i + 1)) {
                this.Add(value[i]);
            }
        }
        
        /// <summary>
        ///     <para>
        ///       Adds the contents of another <see cref='SecurityList'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='SecurityList'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='SecurityList.Add'/>
        public void AddRange(SecurityList value) {
            for (int i = 0; (i < value.Count); i = (i + 1)) {
                this.Add(value[i]);
            }
        }
        
        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='SecurityList'/> contains the specified <see cref='SecurityInfo'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='SecurityInfo'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='SecurityInfo'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='SecurityList.IndexOf'/>
        public bool Contains(SecurityInfo value) {
            return List.Contains(value);
        }
        
        /// <summary>
        /// <para>Copies the <see cref='SecurityList'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='SecurityList'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='SecurityList'/> is greater than the available space between <paramref name='index'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(SecurityInfo[] array, int index) {
            List.CopyTo(array, index);
        }
        
        /// <summary>
        ///    <para>Returns the index of a <see cref='SecurityInfo'/> in 
        ///       the <see cref='SecurityList'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='SecurityInfo'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='SecurityInfo'/> of <paramref name='value'/> in the 
        /// <see cref='SecurityList'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='SecurityList.Contains'/>
        public int IndexOf(SecurityInfo value) {
            return List.IndexOf(value);
        }
        
        /// <summary>
        /// <para>Inserts a <see cref='SecurityInfo'/> into the <see cref='SecurityList'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='SecurityInfo'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='SecurityList.Add'/>
        public void Insert(int index, SecurityInfo value) {
            List.Insert(index, value);
        }
        
        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='SecurityList'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new SecurityInfoEnumerator GetEnumerator() {
            return new SecurityInfoEnumerator(this);
        }
        
        /// <summary>
        ///    <para> Removes a specific <see cref='SecurityInfo'/> from the 
        ///    <see cref='SecurityList'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='SecurityInfo'/> to remove from the <see cref='SecurityList'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(SecurityInfo value) {
            List.Remove(value);
        }
        
        public class SecurityInfoEnumerator : object, IEnumerator {
            
            private IEnumerator baseEnumerator;
            
            private IEnumerable temp;
            
            public SecurityInfoEnumerator(SecurityList mappings) {
                this.temp = ((IEnumerable)(mappings));
                this.baseEnumerator = temp.GetEnumerator();
            }
            
            public SecurityInfo Current {
                get {
                    return ((SecurityInfo)(baseEnumerator.Current));
                }
            }
            
            object IEnumerator.Current {
                get {
                    return baseEnumerator.Current;
                }
            }
            
            public bool MoveNext() {
                return baseEnumerator.MoveNext();
            }
            
            bool IEnumerator.MoveNext() {
                return baseEnumerator.MoveNext();
            }
            
            public void Reset() {
                baseEnumerator.Reset();
            }
            
            void IEnumerator.Reset() {
                baseEnumerator.Reset();
            }
        }
    }
}
