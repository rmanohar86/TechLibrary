<template>

    <div>
        <b-card title="Card Title" no-body>
            <b-card-header header-tag="nav">
                <b-nav card-header tabs align="center">
                    <b-nav-item :to="{ name: 'Home_with_pages' }">Home</b-nav-item>
                    <b-nav-item :to="{ name: 'search_view' }">Search</b-nav-item>
                    <b-nav-item :to="{ name: 'add_new' }">Add New Book</b-nav-item>
                </b-nav>
            </b-card-header>
        </b-card>
    </div>
</template>
<script>
    export default {
        name: 'Navbar'
    };
</script>