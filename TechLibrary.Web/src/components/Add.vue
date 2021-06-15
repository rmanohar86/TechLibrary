<template>
    <div>
        <h2> Enter the new Book details </h2>
        <form v-on:submit.prevent="submitForm">
            <div class="form-group">
                <label for="book_title">Book Title</label>
                <input type="text" class="form-control" id="book_title" placeholder="Book Title" v-model="form.booktitle">
            </div>
            <div class="form-group">
                <label for="isbn">ISBN</label>
                <input type="text" class="form-control" id="isbn" placeholder="ISBN" v-model="form.isbn">
            </div>
            <div class="form-group">
                <label for="book_image">Book Image URL</label>
                <input type="text" class="form-control" id="book_image" placeholder="Book Image URL" v-model="form.imageurl">
            </div>
            <div class="form-group">
                <label for="publishedDate">PublishedDate</label>
                <input type="text" class="form-control" id="publishedDate" placeholder="PublishedDate" v-model="form.publishedDate">
            </div>
            <div class="form-group">
                <label for="message">Description</label>
                <textarea name="description" class="form-control" id="description" rows="3"
                          v-model="form.description" placeholder="Description"></textarea>
            </div>
            <div class="form-group">
                <button class="btn btn-primary">Submit</button>
            </div>
            <section v-if="errormsg">
                <p class="active text-danger" style="font-size:20px">We're sorry, we're not able to add the book details at the moment, please try again later.</p>
            </section>
            <section v-if="successmsg">
                <p class="active text-success" style="font-size:20px">Book details added successfully.</p>
            </section>
        </form>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        name: 'PostFormAxios',       
        data() {
            return {
                form: {
                    book_title: '',
                    isbn: '',
                    book_image: '',
                    description: '',
                    publishedDate: ''
                },
                errormsg: false,
                successmsg: false
            }
        },
        methods: {
            submitForm() {
                this.errormsg = false;
                this.successmsg = false;
                const _book = {
                    BookId: 2,
                    ShortDescr: this.form.description,
                    Title: this.form.booktitle,
                    ISBN: this.form.isbn,
                    PublishedDate: this.form.publishedDate,
                    ThumbnailUrl: this.form.imageurl,
                    LongDescr: null
                };
                console.log("Book : ", _book);
                axios.post(`https://localhost:5001/books/add`, _book)
                    .then(response => {
                        this.successmsg = true;
                        this.form.description = "";
                        this.form.booktitle = "";
                        this.form.isbn = "";
                        this.form.publishedDate = "";
                        this.form.imageurl = "";
                        console.log("Book details added successfully, " + response.data);
                    }).catch(error => {
                        this.errormsg = true;
                        console.log("Failed with error : " + error);
                    });
            }
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
