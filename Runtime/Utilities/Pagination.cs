using System.Collections.Generic;

using UnityEngine;

namespace MuffinDev.Core
{

    /// <summary>
    /// Utility class for pagination system.
    /// </summary>
    [System.Serializable]
    public struct Pagination
    {

        #region Properties

        public const int DEFAULT_NB_ELEMENTS_PER_PAGE = 25;

        [SerializeField]
        private int m_Page;

        [SerializeField]
        private int m_NbElementsPerPage;

        private int m_NbElements;

        #endregion


        #region Initialization

        /// <summary>
        /// Creates a Pagination instance.
        /// </summary>
        /// <param name="_Page">The current page.</param>
        /// <param name="_NbElementsPerPage">The number of elements displayed per page.</param>
        public Pagination(int _Page, int _NbElementsPerPage = DEFAULT_NB_ELEMENTS_PER_PAGE)
        {
            m_Page = _Page;
            m_NbElements = 0;
            m_NbElementsPerPage = _NbElementsPerPage;
        }

        /// <summary>
        /// Creates a Pagination instance.
        /// </summary>
        /// <param name="_NbElements">The number of elements in your paginated ensemble.</param>
        /// <param name="_Page">The current page.</param>
        /// <param name="_NbElementsPerPage">The number of elements displayed per page.</param>
        public Pagination(int _NbElements, int _Page, int _NbElementsPerPage = DEFAULT_NB_ELEMENTS_PER_PAGE)
        {
            m_Page = _Page;
            m_NbElements = _NbElements;
            m_NbElementsPerPage = _NbElementsPerPage;
        }

        #endregion


        #region Public API

        /// <summary>
        /// Creates a sub-list of the given list that contains only the elements that should be displayed using this Pagination data.
        /// </summary>
        /// <typeparam name="T">The type of elements in the given list.</typeparam>
        /// <param name="_List">The list that is paginated.</param>
        /// <returns>Returns the sub-list of the elements to display.</returns>
        public T[] Paginate<T>(IList<T> _List)
        {
            NbElements = _List.Count;
            List<T> subList = new List<T>();
            for (int i = FirstIndex; i < LastIndex; i++)
                subList.Add(_List[i]);
            return subList.ToArray();
        }

        /// <summary>
        /// Creates a sub-list of the given list that contains only the elements that should be displayed using the given pagination
        /// settings.
        /// </summary>
        /// <typeparam name="T">The type of elements in the given list.</typeparam>
        /// <param name="_List">The list that is paginated.</param>
        /// <param name="_Page">The current page.</param>
        /// <param name="_NbElementsPerPage">The number of elements displayed per page.</param>
        /// <returns>Returns the sub-list of the elements to display.</returns>
        public static T[] Paginate<T>(IList<T> _List, int _Page, int _NbElementsPerPage = DEFAULT_NB_ELEMENTS_PER_PAGE)
        {
            Pagination pagination = new Pagination(_List.Count, _Page, _NbElementsPerPage);
            return pagination.Paginate(_List);
        }

        /// <summary>
        /// Creates a sub-list of the given list that contains only the elements that should be displayed using the given pagination
        /// settings.
        /// </summary>
        /// <typeparam name="T">The type of elements in the given list.</typeparam>
        /// <param name="_List">The list that is paginated.</param>
        /// <param name="_Pagination">The Pagination infos of the operation.</param>
        /// <param name="_Page">The current page.</param>
        /// <param name="_NbElementsPerPage">The number of elements displayed per page.</param>
        /// <returns>Returns the sub-list of the elements to display.</returns>
        public static T[] Paginate<T>(IList<T> _List, out Pagination _Pagination, int _Page, int _NbElementsPerPage = DEFAULT_NB_ELEMENTS_PER_PAGE)
        {
            _Pagination = new Pagination(_List.Count, _Page, _NbElementsPerPage);
            return _Pagination.Paginate(_List);
        }

        /// <summary>
        /// Computes the number of pages, given the total number of elements and the number of elements to show per page.
        /// </summary>
        /// <param name="_List">The list that is paginated.</param>
        /// <param name="_NbElementsPerPage">The number of elements displayed per page.</param>
        public static int PagesCount<T>(IList<T> _List, int _NbElementsPerPage = DEFAULT_NB_ELEMENTS_PER_PAGE)
        {
            return Mathf.CeilToInt(_List.Count / (float)_NbElementsPerPage);
        }

        /// <summary>
        /// Computes the number of pages, given the total number of elements and the number of elements to show per page.
        /// </summary>
        /// <param name="_NbElements">The number of elements in your paginated ensemble.</param>
        /// <param name="_NbElementsPerPage">The number of elements displayed per page.</param>
        public static int PagesCount(int _NbElements, int _NbElementsPerPage = DEFAULT_NB_ELEMENTS_PER_PAGE)
        {
            return Mathf.CeilToInt(_NbElements / (float)_NbElementsPerPage);
        }

        #endregion


        #region Accessors

        /// <summary>
        /// Gets/sets the current page index (0-based).
        /// </summary>
        public int Page
        {
            get { return Mathf.Clamp(m_Page, 0, Mathf.Max(0, NbPages - 1)); }
            set { m_Page = value; }
        }

        /// <summary>
        /// Gets the number of pages.
        /// </summary>
        public int NbPages
        {
            get { return PagesCount(m_NbElements, m_NbElementsPerPage); }
        }

        /// <summary>
        /// Gets/sets the total number of elements.
        /// </summary>
        public int NbElements
        {
            get { return m_NbElements; }
            set { m_NbElements = value; }
        }

        /// <summary>
        /// Gets/sets the number of elements displayed per page.
        /// </summary>
        public int NbElementsPerPage
        {
            get { return Mathf.Max(1, m_NbElementsPerPage); }
            set { m_NbElementsPerPage = value; }
        }

        /// <summary>
        /// Gets the first index (inclusive) of your paginated list that should be displayed.
        /// </summary>
        public int FirstIndex
        {
            get { return Page * NbElementsPerPage; }
        }

        /// <summary>
        /// Gets the last index (exclusive) of your paginated list that should be displayed.
        /// </summary>
        public int LastIndex
        {
            get { return Mathf.Min(FirstIndex + NbElementsPerPage, NbElements); }
        }

        #endregion


        #region Operators

        /// <summary>
        /// Checks if the given paginations are equal.
        /// </summary>
        public static bool operator == (Pagination _A, Pagination _B)
        {
            return
                _A.Page == _B.Page &&
                _A.NbElements == _B.NbElements &&
                _A.NbElementsPerPage == _B.NbElementsPerPage;
        }

        /// <summary>
        /// Checks if the given paginations are different.
        /// </summary>
        public static bool operator != (Pagination _A, Pagination _B)
        {
            return
                _A.Page != _B.Page ||
                _A.NbElements != _B.NbElements ||
                _A.NbElementsPerPage == _B.NbElementsPerPage;
        }

        /// <summary>
        /// Increment the current page number.
        /// </summary>
        public static Pagination operator ++(Pagination _Pagination)
        {
            _Pagination.Page++;
            return _Pagination;
        }

        /// <summary>
        /// Decrement the current page number.
        /// </summary>
        public static Pagination operator --(Pagination _Pagination)
        {
            _Pagination.Page--;
            return _Pagination;
        }

        /// <summary>
        /// Adds the given number of pages to the current one.
        /// </summary>
        public static Pagination operator +(Pagination _Pagination, int _NbPagesNext)
        {
            _Pagination.Page += _NbPagesNext;
            return _Pagination;
        }

        /// <summary>
        /// Substracts the given number of pages to the current one.
        /// </summary>
        public static Pagination operator -(Pagination _Pagination, int _NbPagesPrevious)
        {
            _Pagination.Page -= _NbPagesPrevious;
            return _Pagination;
        }

        /// <summary>
        /// Checks if the given object is equal to this Pagination object.
        /// </summary>
        public override bool Equals(object _Other)
        {
            return (_Other != null) && !GetType().Equals(_Other.GetType())
                ? this == (Pagination)_Other
                : false;
        }

        /// <summary>
        /// Gets the Hash Code of this Pagination object.
        /// </summary>
        public override int GetHashCode()
        {
            return Page ^ NbElements ^ NbElementsPerPage;
        }

        /// <summary>
        /// Converts this Pagination object into a string.
        /// </summary>
        public override string ToString()
        {
            return $"Pagination (page: {Page}/{NbPages}, nb. elements: {NbElements}, nb. elements per page: {NbElementsPerPage})";
        }

        #endregion

    }

}