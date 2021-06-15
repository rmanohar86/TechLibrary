<template>
    <div class="homeWithPagination">

        <div class="col-md-12">
            <b-pagination :total-rows="totalRows"
                          :per-page="10"
                          prev-text="Prev"
                          next-text="Next"
                          @change="handlePageChange"></b-pagination>
        </div>

        <section v-if="errored">
            <p class="active text-danger"  style="font-size:20px"  >We're sorry, we're not able to retrieve books information at the moment, please try again later.</p>
        </section>

        <b-table striped hover :items="dataContext" :fields="fields" ref="table" responsive="sm">
            <template v-slot:cell(thumbnailUrl)="data">
                <b-img :src="data.value" thumbnail fluid></b-img>
            </template>
            <template v-slot:cell(title_link)="data">
                <b-link :to="{ name: 'book_view', params: { 'id' : data.item.bookId } }">{{ data.item.title }}</b-link>
            </template>
        </b-table>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        name: 'HomeWithPagination',
        data: () => ({
            fields: [
                { key: 'thumbnailUrl', label: 'Book Image' },
                { key: 'title_link', label: 'Book Title', sortable: true, sortDirection: 'desc' },
                { key: 'isbn', label: 'ISBN', sortable: true, sortDirection: 'desc' },
                { key: 'descr', label: 'Description', sortable: true, sortDirection: 'desc' }
            ],
            items: [],
            pageNumber: [],
            totalRows: 0,
            errored : false
        }),

        methods: {
            dataContext(ctx, callback) {
                axios.get("https://localhost:5001/books/getallbooks", {
                    params: {
                        'pageNumber': 1
                    }
                }).then(response => {
                    callback(response.data.data);
                    this.items = response.data.data;
                    this.pageNumber = response.data.pageNumber;
                    this.totalRows = response.data.totalRows;
                }).catch(error => {
                    console.log(error);
                    this.errored = true
                });
            },
            handlePageChange(value) {
                this.page = value;  
                axios.get("https://localhost:5001/books/getallbooks", {
                    params: {
                        'pageNumber': value
                    }
                }).then(response => {
                    this.pageNumber = response.data.pageNumber;
                    this.totalRows = response.data.totalRows;
                    this.$refs.table.items = response.data.data;
                    this.$refs.table.refresh();
                }).catch(error => {
                    console.log(error);
                    this.errored = true
                });
            }
        }
    };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
