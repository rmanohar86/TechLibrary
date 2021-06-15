<template>
    <div class="homeWithPagination">

        <div class="list row">
            <div class="col-md-8">
                <div class="input-group mb-3">
                    <input type="text"
                           class="form-control"
                           placeholder="Search by title or Description" align="center"
                           v-model="searchString" />
                    <div class="input-group-append">
                        <b-button v-on:click="retrieveTitles" variant="primary">Search</b-button>
                    </div>
                </div>
            </div>

            <section v-if="errored">
                <p class="active text-danger" style="font-size:20px">We're sorry, we're not able to retrieve books information at the moment, please try again later.</p>
            </section>

            <section v-if="noresults">
                <p class="active text-danger" style="font-size:20px">No books found for the given search string.</p>
            </section>

            <b-table striped hover :items="items" :fields="fields" ref="table" responsive="sm">
                <template v-slot:cell(thumbnailUrl)="data">
                    <b-img :src="data.value" thumbnail fluid></b-img>
                </template>
                <template v-slot:cell(title_link)="data">
                    <b-link :to="{ name: 'book_view', params: { 'id' : data.item.bookId } }">{{ data.item.title }}</b-link>
                </template>
            </b-table>

        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        name: 'SearchTitle',
        data: () => ({
            fields: [
                { key: 'thumbnailUrl', label: 'Book Image' },
                { key: 'title_link', label: 'Book Title', sortable: true, sortDirection: 'desc' },
                { key: 'isbn', label: 'ISBN', sortable: true, sortDirection: 'desc' },
                { key: 'descr', label: 'Description', sortable: true, sortDirection: 'desc' }
            ],
            items: [],
            pageNumber: [],
            totalRows: [],
            searchString: '',
            errored: false,
            noresults: false
        }),

        methods: {
            retrieveTitles() {  
                this.noresults = false;
                axios.get("https://localhost:5001/books/search", {
                    params: {
                        'searchString': this.searchString
                    }
                }).then(response => {
                    if (response.data.length == 0) {
                        this.noresults = true;
                    }
                    else {
                        this.$refs.table.items = response.data;
                        this.$refs.table.refresh();
                    }
                }).catch(error => {
                    console.log(error);
                    this.errored = true;
                });
            }
        }
    };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
