import Vue from 'vue';
import VueRouter from 'vue-router';

Vue.use(VueRouter);

//const Home = () => import(/* webpackChunkName: "Home" */ './components/Home.vue');
const HomeWithPagination = () => import(/* webpackChunkName: "HomeWithPagination" */ './components/HomeWithPagination.vue');
const search = () => import(/* webpackChunkName: "HomeWithPagination" */ './components/Search.vue');
const add = () => import(/* webpackChunkName: "HomeWithPagination" */ './components/Add.vue');
const Book = () => import(/* webpackChunkName: "Book" */ './components/Book.vue');

const router = new VueRouter({
  routes: [
    {
            name: 'Home_with_pages',
            path: '/',
            component: HomeWithPagination,
            props: route => ({ query: route.query.pageNumber })
    },
    {
            name: 'search_view',
            path: '/',
            component: search,
            props: route => ({ query: route.query.pageNumber })
    },
    {
            name: 'add_new',
            path: '/',
            component: add,
            props: true
    },
    {
      name: 'book_view',
      path: '/book/:id',
      component: Book,
      props: true
    }
  ]
});

export default router;