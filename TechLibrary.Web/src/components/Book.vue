<template>
    <div v-if="book">
        <b-card :title="book.title"
                :img-src="book.thumbnailUrl"
                img-alt="Image"
                img-top
                tag="article"
                style="max-width: 30rem;"
                class="mb-2">
            <section v-if="displayLabel" >
                <b-card-text v-model="labeltxt">
                    {{ book.descr }}
                </b-card-text>
            </section>
            <section v-if="displayTextArea">
                <b-form-textarea id="textarea-auto-height"
                                 v-model="txtareatxt"
                                 placeholder="Auto height textarea"
                                 rows="3"
                                 max-rows="8"></b-form-textarea>
            </section>
            <br />
            <p style="text-align:center;">
                <span>
                    <b-button to="/" variant="primary">Back</b-button>
                </span>
                &nbsp;&nbsp;
                <span>
                    <b-button v-if="editButttonVisible" v-on:click="editDesc" variant="primary">Edit</b-button>
                </span>
                <span>
                    <b-button v-if="updateButttonVisible" v-on:click="updateDesc" variant="primary">Update</b-button>
                </span>
            </p>
        </b-card>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        name: 'Book',
        props: ["id"],
        data: () => ({
            book: null,
            txtareatxt: null,
            displayTextArea: false,
            displayLabel: true,
            editButttonVisible: true,
            updateButttonVisible: false
        }),
        mounted() {
            axios.get(`https://localhost:5001/books/${this.id}`)
                .then(response => {
                    this.book = response.data;
                    this.txtareatxt = response.data.descr;
                });
        },
        methods: {
            editDesc() {
                this.displayTextArea = true;
                this.displayLabel = false;
                this.editButttonVisible = false;
                this.updateButttonVisible = true;
            },
            updateDesc() {
                const _book = {
                    BookId: this.id,
                    ShortDescr: this.txtareatxt,
                    Title: null,
                    ISBN: null,
                    PublishedDate: null,
                    ThumbnailUrl: null,
                    LongDescr: null
                };
                axios.post(`https://localhost:5001/books/update`, _book)
                .then(response => {
                    console.log(response.data.descr);
                    this.book = response.data;
                    this.displayTextArea = false;
                    this.displayLabel = true;
                    this.editButttonVisible = true;
                    this.updateButttonVisible = false;
                });
            }
        }
    }
</script>