<template>
    <div class="max-w-xl mx-auto mt-10 p-6 bg-white rounded shadow">
        <h1 class="text-2xl font-bold mb-4">📝 Todo App</h1>

        <form @submit.prevent="addTodo" class="flex gap-2 mb-4">
            <input v-model="newTodo"
                   class="flex-1 p-2 border rounded"
                   placeholder="Nhập công việc mới" />
            <button class="px-4 py-2 bg-blue-500 text-white rounded">Thêm</button>
        </form>

        <ul class="space-y-2">
            <li v-for="todo in todos"
                :key="todo.id"
                class="flex justify-between items-center p-2 border rounded">
                <div>
                    <input type="checkbox"
                           v-model="todo.isCompleted"
                           @change="toggleComplete(todo)"
                           class="mr-2" />
                    <span :class="{ 'line-through text-gray-400': todo.isCompleted }">
                        {{ todo.title }}
                    </span>
                </div>
                <button @click="deleteTodo(todo.id)"
                        class="text-red-500 hover:text-red-700">
                    X
                </button>
            </li>
        </ul>
    </div>
</template>

<script setup>
    import { ref, onMounted } from 'vue'

    const API_URL = 'http://localhost:5286/api/Todo' // Địa chỉ backend bạn đã chạy

    const todos = ref([])
    const newTodo = ref('')

    const fetchTodos = async () => {
        const res = await fetch(API_URL)
        todos.value = await res.json()
    }

    const addTodo = async () => {
        if (!newTodo.value.trim()) return
        const res = await fetch(API_URL, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ title: newTodo.value, isCompleted: false })
        })
        const added = await res.json()
        todos.value.push(added)
        newTodo.value = ''
    }

    const deleteTodo = async (id) => {
        await fetch(`${API_URL}/${id}`, { method: 'DELETE' })
        todos.value = todos.value.filter(todo => todo.id !== id)
    }

    const toggleComplete = async (todo) => {
        await fetch(`${API_URL}/${todo.id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(todo)
        })
    }

    onMounted(fetchTodos)
</script>

<style>
    body {
        background-color: #f9fafb;
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    }
</style>
